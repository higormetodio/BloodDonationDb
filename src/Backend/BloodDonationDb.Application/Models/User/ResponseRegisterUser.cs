using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationDb.Application.Models.User;
public class ResponseRegisterUser
{
    public ResponseRegisterUser(string name, string token)
    {
        Name = name;
        Token = token;
    }

    public string Name { get; private set; }
    public string Token { get; private set; }

    public static ResponseRegisterUser FromEntity(Domain.Entities.User user, string token)
        => new ResponseRegisterUser(user.Name, token);
}
