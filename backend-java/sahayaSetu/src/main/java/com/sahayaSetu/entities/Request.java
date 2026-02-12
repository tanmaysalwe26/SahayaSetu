package com.sahayaSetu.entities;

import com.sahayaSetu.entities.enums.RequestStatus;
import com.sahayaSetu.entities.enums.RequestType;
import jakarta.persistence.*;
import lombok.Data;
import org.hibernate.annotations.CreationTimestamp;

import java.time.LocalDateTime;

@Entity
// @Table(name = "items") // Changing name to avoid keyword reserved conflict if
// any, but prompt said
// REQUEST. Let's stick to 'request' but usually it's a keyword. Prompt says
// REQUEST table.
// Actually, 'REQUEST' is often a reserved keyword in SQL. I will use "requests"
// as table name to be safe.
// Re-reading prompt: "REQUEST".
// I'll annotate @Table(name="requests") for safety.
@Table(name = "requests")
@Inheritance(strategy = InheritanceType.JOINED)
@Data
public class Request {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "request_id")
    private Long requestId;

    @ManyToOne
    @JoinColumn(name = "ngo_id", nullable = false)
    private Ngo ngo;

    @Column(nullable = false)
    private String title;

    @Column(columnDefinition = "TEXT")
    private String description;

    @Enumerated(EnumType.STRING)
    @Column(name = "request_type", nullable = false)
    private RequestType requestType;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private RequestStatus status = RequestStatus.OPEN;

    @CreationTimestamp
    @Column(name = "created_at", updatable = false)
    private LocalDateTime createdAt;
}
