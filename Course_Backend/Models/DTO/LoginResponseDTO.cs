﻿namespace Course_Backend.Models.DTO
{
    public class LoginResponseDTO
    {
        public LocalUser User { get; set; } 

        public string Token { get; set; }   
    }
}
