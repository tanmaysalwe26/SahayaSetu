package com.sahayaSetu.services;

import com.sahayaSetu.dtos.*;

import java.util.List;

public interface INgoService {
    void createResourceRequest(Long ngoId, ResourceRequestDto dto);
    void createVolunteerRequest(Long ngoId, VolunteerRequestDto dto);
    void createFundraiserRequest(Long ngoId, FundraiserRequestDto dto);
    List<RequestResponseDto> getNgoRequests(Long ngoId);
    List<DonationResponseDto> getNgoDonations(Long ngoId);
    List<DonationResponseDto> getFundraiserDonations(Long ngoId);
    void sendDonationResponse(DonationResponseRequestDto dto);
    void updateNgoProfile(Long ngoId, NgoUpdateDto dto);
    void closeRequest(Long requestId, Long ngoId);
    Object getRequestDetails(Long requestId);
}
