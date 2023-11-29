using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using midterm.Model;

namespace Repository
{
    public class PassengerRepository
    {
        private IConfiguration _configuration;
        public PassengerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Passenger? CreatePassenger(SignUp modal)
        {
            using var context = new Se4458midtermContext();
            try
            {
                var user = new Passenger
                {
                    FirstName = modal.FirstName,
                    LastName = modal.LastName,
                    Username = modal.Username,
                    PassengerPassword = modal.Password
                };
                user = CreateToken(user);

                context.Passengers.Add(user);
                context.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Passenger? GetPassengerByUsername(string username)
        {
            using var context = new Se4458midtermContext();
            try
            {
                var user = context.Passengers.Single(u => u.Username == username);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Passenger? GetPassengerByToken(string token)
        {
            using var context = new Se4458midtermContext();
            try
            {
                var user = context.Passengers.Single(u => u.Token == token);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Passenger? GetPassengerLogin(Login login)
        {
            using var context = new Se4458midtermContext();
            try
            {
                var user = context.Passengers.Single(u => u.Username == login.Username && u.PassengerPassword == login.Password);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private Passenger CreateToken(Passenger user)
        {
            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Email, user.Username),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var token = GetToken(authClaims);
            user.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return user;
        }
        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMonths(12),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}