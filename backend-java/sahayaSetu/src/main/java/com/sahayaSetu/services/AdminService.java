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
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class AdminService {

    @Autowired
    private NgoRepository ngoRepository;

    @Autowired
    private RequestRepository requestRepository;

    @Autowired
    private ModelMapper modelMapper;

    public List<NgoResponseDto> getAllNgos() {
        return ngoRepository.findAll().stream()
                .map(ngo -> modelMapper.map(ngo, NgoResponseDto.class))
                .collect(Collectors.toList());
    }

    public List<RequestResponseDto> getAllRequests() {
        return requestRepository.findAll().stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    @Autowired
    private DonorRepository donorRepository;

    public List<DonorResponseDto> getAllDonors() {
        return donorRepository.findAll().stream()
                .map(donor -> {
                    DonorResponseDto dto = modelMapper.map(donor, DonorResponseDto.class);
                    dto.setEmail(donor.getUser().getEmail());
                    return dto;
                })
                .collect(Collectors.toList());
    }

    private RequestResponseDto mapToDto(Request request) {
        if (request instanceof VolunteerRequest) {
            VolunteerRequest vr = (VolunteerRequest) request;
            VolunteerRequestResponseDto dto = modelMapper.map(vr, VolunteerRequestResponseDto.class);
            dto.setDescription(vr.getDescription());
            dto.setSkillsRequired(vr.getSkillsRequired());
            dto.setVolunteersRequired(vr.getVolunteersRequired());
            return dto;
        } else if (request instanceof ResourceRequest) {
            ResourceRequest rr = (ResourceRequest) request;
            ResourceRequestResponseDto dto = modelMapper.map(rr, ResourceRequestResponseDto.class);
            dto.setResourceType(rr.getResourceType());
            dto.setQuantityRequired(rr.getQuantityRequired());
            dto.setQuantityReceived(rr.getQuantityReceived());
            return dto;
        } else if (request instanceof FundraiserRequest) {
            FundraiserRequest fr = (FundraiserRequest) request;
            FundraiserRequestResponseDto dto = modelMapper.map(fr, FundraiserRequestResponseDto.class);
            dto.setTargetAmount(fr.getTargetAmount());
            dto.setCollectedAmount(fr.getCollectedAmount());
            dto.setDeadline(fr.getDeadline());
            return dto;
        } else {
            return modelMapper.map(request, RequestResponseDto.class);
        }
    }

    @Autowired
    private MailService emailService;

    @Transactional
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

    @Transactional
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

    @Transactional
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

    @Transactional
    public void enableNgo(Long ngoId) {
        Ngo ngo = ngoRepository.findById(ngoId)
                .orElseThrow(() -> new RuntimeException("NGO not found"));
        ngo.setStatus(NgoStatus.APPROVED);
        ngoRepository.save(ngo);
    }
}
