package com.sahayaSetu.controllers;

import com.sahayaSetu.services.IAdminService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;

import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/admin")
@RequiredArgsConstructor
public class AdminController {

    private final IAdminService adminService;

    @GetMapping("/ngos")
    public ResponseEntity<?> getAllNgos() {
        return ResponseEntity.ok(adminService.getAllNgos());
    }

    @GetMapping("/requests")
    public ResponseEntity<?> getAllRequests() {
        return ResponseEntity.ok(adminService.getAllRequests());
    }

    @GetMapping("/donors")
    public ResponseEntity<?> getAllDonors() {
        return ResponseEntity.ok(adminService.getAllDonors());
    }

    @PutMapping("/ngos/{id}/approve")
    public ResponseEntity<?> approveNgo(@PathVariable Long id) {
        adminService.approveNgo(id);
        return ResponseEntity.ok("NGO approved successfully");
    }

    @PutMapping("/ngos/{id}/disapprove")
    public ResponseEntity<?> disapproveNgo(@PathVariable Long id) {
        adminService.disapproveNgo(id);
        return ResponseEntity.ok("NGO disapproved successfully");
    }

    @PutMapping("/ngos/{id}/disable")
    public ResponseEntity<?> disableNgo(@PathVariable Long id) {
        adminService.disableNgo(id);
        return ResponseEntity.ok("NGO disabled successfully");
    }

    @PutMapping("/ngos/{id}/enable")
    public ResponseEntity<?> enableNgo(@PathVariable Long id) {
        adminService.enableNgo(id);
        return ResponseEntity.ok("NGO enabled successfully");
    }
}
