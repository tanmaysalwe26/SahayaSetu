package com.sahayaSetu.services;

import com.sahayaSetu.dtos.*;

public interface IAuthService {
    AuthResponseDto registerDonor(DonorRegistrationDto dto);
    AuthResponseDto registerNgo(NgoRegistrationDto dto);
    AuthResponseDto loginUser(LoginDto dto);
    AuthResponseDto loginNgo(LoginDto dto);
    AuthResponseDto registerAdmin(AdminRegistrationDto dto);
}
