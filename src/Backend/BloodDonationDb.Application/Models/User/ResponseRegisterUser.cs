using BloodDonationDb.Application.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationDb.Application.Models.User;
public class ResponseRegisterUser
{
    public string? Name { get; set; }
    public string? Token { get; set; }
}
