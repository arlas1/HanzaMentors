﻿namespace WebAppApi.Models;

public class GenerateDoucmentDTO
{
    public string MenteeId { get; set; }
    public List<string>? SelectedSamples { get; set; }
    public string? SelectedMentorId { get; set; }
    public List<string>? SigningTimes { get; set; }
    
    public string IsTest { get; set; }
    // public string TestSigningTimes { get; set; }
    // public string TestSelectedSampleId { get; set; }
}