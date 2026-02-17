package com.sahayaSetu.services;

import com.sahayaSetu.dtos.*;
import com.sahayaSetu.entities.*;
import com.sahayaSetu.entities.enums.NgoStatus;
import com.sahayaSetu.entities.enums.Role;
import com.sahayaSetu.repositories.*;
import com.sahayaSetu.utils.JwtUtils;
import lombok.RequiredArgsConstructor;
import org.modelmapper.ModelMapper;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@Transactional
@RequiredArgsConstructor
public class AuthServiceImpl implements IAuthService {

    private final UserRepository userRepository;
    private final DonorRepository donorRepository;
    private final NgoRepository ngoRepository;
    private final PasswordEncoder passwordEncoder;
    private final JwtUtils jwtUtils;
    private final ModelMapper modelMapper;

    public AuthResponseDto registerDonor(DonorRegistrationDto dto) {
        if (userRepository.existsByEmail(dto.getEmail())) {
            throw new RuntimeException("Email already active");
        }

        User user = modelMapper.map(dto, User.class);
        user.setPasswordHash(passwordEncoder.encode(dto.getPassword()));
        user.setRole(Role.DONOR);
        user.setActive(true);
        user = userRepository.save(user);

        Donor donor = modelMapper.map(dto, Donor.class);
        donor.setUser(user);
        donorRepository.save(donor);

        String token = jwtUtils.generateToken(user.getEmail(), user.getRole().name(), user.getUserId());
        return new AuthResponseDto(token, user.getRole().name(), "Donor registered successfully");
    }

    public AuthResponseDto registerNgo(NgoRegistrationDto dto) {
        if (ngoRepository.existsByEmail(dto.getEmail())) {
            throw new RuntimeException("Email already registered");
        }
        if (ngoRepository.existsByDarpanId(dto.getDarpanId())) {
            throw new RuntimeException("Darpan ID already registered");
        }

        Ngo ngo = modelMapper.map(dto, Ngo.class);
        ngo.setPasswordHash(passwordEncoder.encode(dto.getPassword()));
        ngo.setStatus(NgoStatus.PENDING);

        ngoRepository.save(ngo);

        return new AuthResponseDto(null, Role.NGO.name(),
                "NGO registration successful. Please wait for Admin approval.");
    }

    public AuthResponseDto loginUser(LoginDto dto) {
        User user = userRepository.findByEmail(dto.getEmail())
                .orElseThrow(() -> new RuntimeException("User not found"));

        if (!passwordEncoder.matches(dto.getPassword(), user.getPasswordHash())) {
            throw new RuntimeException("Invalid credentials");
        }
        if (!user.isActive()) {
            throw new RuntimeException("Account is inactive");
        }

        Long tokenPayloadId = user.getUserId();
        if (user.getRole() == Role.DONOR) {
            Donor donor = donorRepository.findByUser(user)
                    .orElseThrow(() -> new RuntimeException("Donor profile not found"));
            tokenPayloadId = donor.getDonorId();
        }

        String token = jwtUtils.generateToken(user.getEmail(), user.getRole().name(), tokenPayloadId);
        return new AuthResponseDto(token, user.getRole().name(), "Login successful");
    }

    public AuthResponseDto loginNgo(LoginDto dto) {
        Ngo ngo = ngoRepository.findByEmail(dto.getEmail())
                .orElseThrow(() -> new RuntimeException("NGO not found"));

        if (!passwordEncoder.matches(dto.getPassword(), ngo.getPasswordHash())) {
            throw new RuntimeException("Invalid credentials");
        }

        if (ngo.getStatus() != NgoStatus.APPROVED) {
            throw new RuntimeException("NGO account is not approved yet. Current status: " + ngo.getStatus());
        }

        String token = jwtUtils.generateToken(ngo.getEmail(), Role.NGO.name(), ngo.getNgoId());
        return new AuthResponseDto(token, Role.NGO.name(), "Login successful");
    }

    public AuthResponseDto registerAdmin(AdminRegistrationDto dto) {
        if (userRepository.existsByEmail(dto.getEmail())) {
            throw new RuntimeException("Email already active");
        }

        User user = modelMapper.map(dto, User.class);
        user.setPasswordHash(passwordEncoder.encode(dto.getPassword()));
        user.setRole(Role.ADMIN);
        user.setActive(true);
        userRepository.save(user);

        String token = jwtUtils.generateToken(user.getEmail(), user.getRole().name(), user.getUserId());
        return new AuthResponseDto(token, user.getRole().name(), "Admin registered successfully");
    }
}
