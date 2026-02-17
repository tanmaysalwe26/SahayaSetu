package com.sahayaSetu.services;

import com.sahayaSetu.dtos.NgoResponseDto;
import com.sahayaSetu.dtos.RequestResponseDto;
import com.sahayaSetu.dtos.DonorResponseDto;

import java.util.List;

public interface IAdminService {
    List<NgoResponseDto> getAllNgos();
    List<RequestResponseDto> getAllRequests();
    List<DonorResponseDto> getAllDonors();
    void approveNgo(Long ngoId);
    void disapproveNgo(Long ngoId);
    void disableNgo(Long ngoId);
    void enableNgo(Long ngoId);
}
