package com.sahayaSetu.controllers;

import com.sahayaSetu.dtos.DonationDto;
import com.sahayaSetu.dtos.ResourceFulfillmentDto;
import com.sahayaSetu.services.IDonorService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/donor")
@RequiredArgsConstructor
public class DonorController {

    private final IDonorService donorService;

    @GetMapping("/requests")
    public ResponseEntity<?> getAllOpenRequests() {
        return ResponseEntity.ok(donorService.getAllOpenRequests());
    }

    @GetMapping("/requests-for-donor")
    public ResponseEntity<?> getAllOpenRequestsForDonor(@RequestParam Long donorId) {
        return ResponseEntity.ok(donorService.getAllOpenRequestsForDonor(donorId));
    }

    @GetMapping("/fundraisers")
    public ResponseEntity<?> getOpenFundraisers() {
        return ResponseEntity.ok(donorService.getOpenFundraisers());
    }

    @PostMapping("/requests/{requestId}/donate")
    public ResponseEntity<?> donate(@PathVariable Long requestId,
            @RequestParam Long donorId,
            @Valid @RequestBody DonationDto dto) {
        donorService.donateToFundraiser(donorId, requestId, dto);
        return ResponseEntity.ok("Donation processed successfully");
    }

    @PostMapping("/requests/{requestId}/volunteer")
    public ResponseEntity<?> volunteer(@PathVariable Long requestId,
            @RequestParam Long donorId) {
        donorService.applyForVolunteer(donorId, requestId);
        return ResponseEntity.ok("Volunteer application submitted");
    }

    @PostMapping("/requests/{requestId}/fulfill-resource")
    public ResponseEntity<?> fulfillResource(@PathVariable Long requestId,
            @RequestParam Long donorId,
            @Valid @RequestBody ResourceFulfillmentDto dto) {
        donorService.fulfillResourceRequest(donorId, requestId, dto.getQuantity());
        return ResponseEntity.ok("Resource contribution recorded");
    }

    @GetMapping("/contributions")
    public ResponseEntity<?> getContributions(@RequestParam Long donorId) {
        try {
            if (donorId == null || donorId <= 0) {
                return ResponseEntity.badRequest().body("Invalid donor ID");
            }
            return ResponseEntity.ok(donorService.getDonorContributions(donorId));
        } catch (RuntimeException e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        } catch (Exception e) {
            return ResponseEntity.status(500).body("Internal server error: " + e.getMessage());
        }
    }
}
