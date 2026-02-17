package com.sahayaSetu.services;

import com.sahayaSetu.dtos.*;

import java.util.List;

public interface IDonorService {
    List<RequestResponseDto> getAllOpenRequests();
    List<RequestResponseDto> getAllOpenRequestsForDonor(Long donorId);
    List<FundraiserRequestResponseDto> getOpenFundraisers();
    void donateToFundraiser(Long donorId, Long requestId, DonationDto dto);
    void applyForVolunteer(Long donorId, Long requestId);
    void fulfillResourceRequest(Long donorId, Long requestId, int quantity);
    ContributionSummaryDto getDonorContributions(Long donorId);
}
