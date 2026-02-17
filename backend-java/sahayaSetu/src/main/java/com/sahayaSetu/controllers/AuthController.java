package com.sahayaSetu.controllers;

import com.sahayaSetu.dtos.*;
import com.sahayaSetu.services.IAuthService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/auth")
@RequiredArgsConstructor
public class AuthController {

    private final IAuthService authService;

    @PostMapping("/register")
    public ResponseEntity<?> registerDonor(@Valid @RequestBody DonorRegistrationDto dto) {
        return ResponseEntity.ok(authService.registerDonor(dto));
    }

    @PostMapping("/register-ngo")
    public ResponseEntity<?> registerNgo(@Valid @RequestBody NgoRegistrationDto dto) {
        return ResponseEntity.ok(authService.registerNgo(dto));
    }

    @PostMapping("/register-admin")
    public ResponseEntity<?> registerAdmin(@Valid @RequestBody AdminRegistrationDto dto) {
        return ResponseEntity.ok(authService.registerAdmin(dto));
    }

    @PostMapping("/login")
    public ResponseEntity<?> login(@Valid @RequestBody LoginDto dto) {
        return ResponseEntity.ok(authService.loginUser(dto));
    }

    @PostMapping("/login-ngo")
    public ResponseEntity<?> loginNgo(@Valid @RequestBody LoginDto dto) {
        return ResponseEntity.ok(authService.loginNgo(dto));
    }
}
