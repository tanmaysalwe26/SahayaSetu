package com.sahayaSetu.services;

import com.sahayaSetu.dtos.NgoResponseDto;
import com.sahayaSetu.dtos.RequestResponseDto;
import com.sahayaSetu.dtos.DonorResponseDto;
import com.sahayaSetu.dtos.VolunteerRequestResponseDto;
import com.sahayaSetu.dtos.ResourceRequestResponseDto;
import com.sahayaSetu.dtos.FundraiserRequestResponseDto;
import com.sahayaSetu.entities.Ngo;
import com.sahayaSetu.entities.Request;
import com.sahayaSetu.entities.VolunteerRequest;
import com.sahayaSetu.entities.ResourceRequest;
import com.sahayaSetu.entities.FundraiserRequest;
import com.sahayaSetu.entities.enums.NgoStatus;
import com.sahayaSetu.repositories.NgoRepository;
import com.sahayaSetu.repositories.RequestRepository;
import com.sahayaSetu.repositories.DonorRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
@Transactional
@RequiredArgsConstructor
public class AdminServiceImpl implements IAdminService {

    private final NgoRepository ngoRepository;
    private final RequestRepository requestRepository;
    private final DonorRepository donorRepository;
    private final MailService emailService;

    public List<NgoResponseDto> getAllNgos() {
        return ngoRepository.findAll().stream()
                .map(ngo -> {
                    NgoResponseDto dto = new NgoResponseDto();
                    dto.setNgoId(ngo.getNgoId());
                    dto.setName(ngo.getName());
                    dto.setEmail(ngo.getEmail());
                    dto.setDarpanId(ngo.getDarpanId());
                    dto.setContactPhone(ngo.getContactPhone());
                    dto.setAddressLine1(ngo.getAddressLine1());
                    dto.setCity(ngo.getCity());
                    dto.setStatus(ngo.getStatus());
                    dto.setApprovedAt(ngo.getApprovedAt());
                    dto.setCreatedAt(ngo.getCreatedAt());
                    return dto;
                })
                .collect(Collectors.toList());
    }

    public List<RequestResponseDto> getAllRequests() {
        return requestRepository.findAll().stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public List<DonorResponseDto> getAllDonors() {
        return donorRepository.findAll().stream()
                .map(donor -> {
                    DonorResponseDto dto = new DonorResponseDto();
                    dto.setDonorId(donor.getDonorId());
                    dto.setFullName(donor.getFullName());
                    dto.setPhone(donor.getPhone());
                    dto.setCity(donor.getCity());
                    dto.setDateOfBirth(donor.getDateOfBirth());
                    dto.setEmail(donor.getUser().getEmail());
                    return dto;
                })
                .collect(Collectors.toList());
    }

    private RequestResponseDto mapToDto(Request request) {
        if (request instanceof VolunteerRequest) {
            VolunteerRequest vr = (VolunteerRequest) request;
            VolunteerRequestResponseDto dto = new VolunteerRequestResponseDto();
            dto.setRequestId(vr.getRequestId());
            dto.setTitle(vr.getTitle());
            dto.setDescription(vr.getDescription());
            dto.setSkillsRequired(vr.getSkillsRequired());
            dto.setVolunteersRequired(vr.getVolunteersRequired());
            dto.setStatus(vr.getStatus());
            dto.setRequestType(vr.getRequestType());
            dto.setCreatedAt(vr.getCreatedAt());
            dto.setNgo(mapNgoToDto(vr.getNgo()));
            return dto;
        } else if (request instanceof ResourceRequest) {
            ResourceRequest rr = (ResourceRequest) request;
            ResourceRequestResponseDto dto = new ResourceRequestResponseDto();
            dto.setRequestId(rr.getRequestId());
            dto.setTitle(rr.getTitle());
            dto.setDescription(rr.getDescription());
            dto.setResourceType(rr.getResourceType());
            dto.setQuantityRequired(rr.getQuantityRequired());
            dto.setQuantityReceived(rr.getQuantityReceived());
            dto.setStatus(rr.getStatus());
            dto.setRequestType(rr.getRequestType());
            dto.setCreatedAt(rr.getCreatedAt());
            dto.setNgo(mapNgoToDto(rr.getNgo()));
            return dto;
        } else if (request instanceof FundraiserRequest) {
            FundraiserRequest fr = (FundraiserRequest) request;
            FundraiserRequestResponseDto dto = new FundraiserRequestResponseDto();
            dto.setRequestId(fr.getRequestId());
            dto.setTitle(fr.getTitle());
            dto.setDescription(fr.getDescription());
            dto.setTargetAmount(fr.getTargetAmount());
            dto.setCollectedAmount(fr.getCollectedAmount());
            dto.setDeadline(fr.getDeadline());
            dto.setStatus(fr.getStatus());
            dto.setRequestType(fr.getRequestType());
            dto.setCreatedAt(fr.getCreatedAt());
            dto.setNgo(mapNgoToDto(fr.getNgo()));
            return dto;
        } else {
            RequestResponseDto dto = new RequestResponseDto();
            dto.setRequestId(request.getRequestId());
            dto.setTitle(request.getTitle());
            dto.setDescription(request.getDescription());
            dto.setStatus(request.getStatus());
            dto.setRequestType(request.getRequestType());
            dto.setCreatedAt(request.getCreatedAt());
            dto.setNgo(mapNgoToDto(request.getNgo()));
            return dto;
        }
    }

    private NgoResponseDto mapNgoToDto(Ngo ngo) {
        NgoResponseDto dto = new NgoResponseDto();
        dto.setNgoId(ngo.getNgoId());
        dto.setName(ngo.getName());
        dto.setEmail(ngo.getEmail());
        dto.setDarpanId(ngo.getDarpanId());
        dto.setContactPhone(ngo.getContactPhone());
        dto.setAddressLine1(ngo.getAddressLine1());
        dto.setCity(ngo.getCity());
        dto.setStatus(ngo.getStatus());
        dto.setApprovedAt(ngo.getApprovedAt());
        dto.setCreatedAt(ngo.getCreatedAt());
        return dto;
    }


    public void approveNgo(Long ngoId) {
        Ngo ngo = ngoRepository.findById(ngoId)
                .orElseThrow(() -> new RuntimeException("NGO not found"));

        if (ngo.getStatus() == NgoStatus.APPROVED) {
            throw new RuntimeException("NGO is already approved");
        }

        ngo.setStatus(NgoStatus.APPROVED);
        ngo.setApprovedAt(LocalDateTime.now());
        ngoRepository.save(ngo);

        emailService.sendNgoApprovalMail(ngo.getEmail(), ngo.getName());
    }

    public void disapproveNgo(Long ngoId) {
        Ngo ngo = ngoRepository.findById(ngoId)
                .orElseThrow(() -> new RuntimeException("NGO not found"));

        if (ngo.getStatus() == NgoStatus.REJECTED) {
            throw new RuntimeException("NGO is already rejected");
        }

        ngo.setStatus(NgoStatus.REJECTED);
        ngoRepository.save(ngo);

        emailService.sendDisapprovalEmail(ngo.getEmail(), ngo.getName());
    }

    public void disableNgo(Long ngoId) {
        Ngo ngo = ngoRepository.findById(ngoId)
                .orElseThrow(() -> new RuntimeException("NGO not found"));
        
        if (ngo.getStatus() == NgoStatus.DISABLED) {
            throw new RuntimeException("NGO is already disabled");
        }
        
        ngo.setStatus(NgoStatus.DISABLED);
        ngoRepository.save(ngo);
        
        emailService.sendNgoDisableEmail(ngo.getEmail(), ngo.getName());
    }

    public void enableNgo(Long ngoId) {
        Ngo ngo = ngoRepository.findById(ngoId)
                .orElseThrow(() -> new RuntimeException("NGO not found"));
        ngo.setStatus(NgoStatus.APPROVED);
        ngoRepository.save(ngo);
    }
}
