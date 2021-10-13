using Sat.Recruitment.Api.Constants;
using Sat.Recruitment.Core.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api
{
    public class UserModel : IUser
    {
        private string _name = "";
        private string _email = "";
        private string _address = "";
        private string _phone = "";
        private decimal _money = 0;

        [Required]
        public string Name 
        { 
            get => _name;
            set
            {
                _name = value.Replace(",", string.Empty);
            } 
        }
        [Required]
        [EmailAddress]
        public string Email 
        { 
            get => _email; 
            set 
            {
                _email = NormalizeEmail(value);
            }
        }
        [Required]
        public string Address 
        { 
            get => _address;
            set
            {
                _address = value.Replace(",", string.Empty);
            }
        }
        [Required]
        public string Phone 
        { 
            get => _phone;
            set
            {
                _phone = value.Replace(" ", string.Empty);
            }
        }
        [RegularExpression(UserTypes.Normal + "|" + UserTypes.Premium + "|" + UserTypes.SuperUser , ErrorMessage = "Characters are not allowed.")]
        public string UserType { get; set; }
        public decimal Money 
        {
            get => _money; 
            set
            {
                _money = AssignMoney(value);
            }
        }

        private decimal AssignMoney(decimal money)
        {
            if (money < 100  && money > 10 && UserType == UserTypes.Normal)
            {
                UserTypesPercentages.UserPercentages.TryGetValue(UserTypes.Normal_better, out decimal percentage);
                return money + ( money * percentage);
            }
            else if (money > 100)
            {
                UserTypesPercentages.UserPercentages.TryGetValue(UserType, out decimal percentage);
                return money + (money * percentage);
            }
            return money;
        }

        private string NormalizeEmail(string email)
        {
            var emailSplitted = email.Split('@', StringSplitOptions.RemoveEmptyEntries);

            var atIndex = emailSplitted[0].IndexOf("+", StringComparison.Ordinal);

            emailSplitted[0] = emailSplitted[0].Replace(".", "");

            if(atIndex >= 0)
                emailSplitted[0] = emailSplitted[0].Remove(atIndex);

            return string.Join("@", new string[] { emailSplitted[0], emailSplitted[1] });
        }
    }
}
