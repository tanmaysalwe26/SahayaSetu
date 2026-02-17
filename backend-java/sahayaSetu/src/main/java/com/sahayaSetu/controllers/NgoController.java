package com.sahayaSetu.controllers;

import com.sahayaSetu.dtos.*;

import com.sahayaSetu.services.INgoService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;

import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/ngo")
@RequiredArgsConstructor
public class NgoController {

    private final INgoService ngoService;

    // Token verification removed for testing as per user request
    // private Long getCurrentNgoId(Authentication authentication) { ... }

    @PostMapping("/requests/resource")
    public ResponseEntity<?> createResourceRequest(@Valid @RequestBody ResourceRequestDto dto,
            @RequestParam Long ngoId) {
        ngoService.createResourceRequest(ngoId, dto);
        return ResponseEntity.ok("Resource Request created successfully");
    }

    @PostMapping("/requests/volunteer")
    public ResponseEntity<?> createVolunteerRequest(@Valid @RequestBody VolunteerRequestDto dto,
            @RequestParam Long ngoId) {
        ngoService.createVolunteerRequest(ngoId, dto);
        return ResponseEntity.ok("Volunteer Request created successfully");
    }

    @PostMapping("/requests/fundraiser")
    public ResponseEntity<?> createFundraiserRequest(@Valid @RequestBody FundraiserRequestDto dto,
            @RequestParam Long ngoId) {
        ngoService.createFundraiserRequest(ngoId, dto);
        return ResponseEntity.ok("Fundraiser Request created successfully");
    }

    @GetMapping("/{ngoId}/requests")
    public ResponseEntity<?> getNgoRequests(@PathVariable Long ngoId) {
        return ResponseEntity.ok(ngoService.getNgoRequests(ngoId));
    }

    @GetMapping("/{ngoId}/donations")
    public ResponseEntity<?> getNgoDonations(@PathVariable Long ngoId) {
        return ResponseEntity.ok(ngoService.getNgoDonations(ngoId));
    }

    @PutMapping("/{ngoId}")
    public ResponseEntity<?> updateProfile(@PathVariable Long ngoId, @RequestBody NgoUpdateDto dto) {
        ngoService.updateNgoProfile(ngoId, dto);
        return ResponseEntity.ok("Profile updated");
    }

    @GetMapping("/fundraiser-donations")
    public ResponseEntity<?> getFundraiserDonations(@RequestParam Long ngoId) {
        return ResponseEntity.ok(ngoService.getFundraiserDonations(ngoId));
    }

    @PostMapping("/donation-response")
    public ResponseEntity<?> sendDonationResponse(@RequestBody DonationResponseRequestDto dto) {
        ngoService.sendDonationResponse(dto);
        return ResponseEntity.ok("Response sent successfully");
    }

    @PutMapping("/requests/{requestId}/close")
    public ResponseEntity<?> closeRequest(@PathVariable Long requestId, @RequestParam Long ngoId) {
        ngoService.closeRequest(requestId, ngoId);
        return ResponseEntity.ok("Request closed successfully");
    }

    @GetMapping("/requests/{requestId}/details")
    public ResponseEntity<?> getRequestDetails(@PathVariable Long requestId) {
        return ResponseEntity.ok(ngoService.getRequestDetails(requestId));
    }
}
