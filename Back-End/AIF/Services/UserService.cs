using System;
using System.Collections.Generic;
using AIF.Models;

namespace AIF.Services
{
    public class UserService
    {
        private List<User> userList = new List<User>();

        public void RunUserManagement()
        {
            DisplayUsers(userList);

            string email = GetDataFromEndpoint("Enter the email of the user to update:");

            User userToUpdate = userList.Find(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (userToUpdate != null)
            {
                string name = GetDataFromEndpoint("Enter the new name:");
                string newEmail = GetDataFromEndpoint("Enter the new email:");

                if (!string.IsNullOrEmpty(name))
                {
                    userToUpdate.Name = name;
                }

                if (!string.IsNullOrEmpty(newEmail))
                {
                    userToUpdate.Email = newEmail;
                }

                DisplayDataFromEndpoint("\nUser updated successfully!");
            }
            else
            {
                DisplayDataFromEndpoint("\nUser not found!");
            }

            DisplayUsers(userList);
        }

        private void DisplayUsers(List<User> users)
        {
            DisplayDataFromEndpoint("Existing Users:");
            foreach (var user in users)
            {
                DisplayDataFromEndpoint($"{user.Name} - {user.Email}");
            }
        }

        private string GetDataFromEndpoint(string prompt)
        {
            DisplayDataFromEndpoint(prompt);
            return "Data from the endpoint";
        }

        private void DisplayDataFromEndpoint(string data)
        {
            CustomDisplayLogic(data);
        }

        private void CustomDisplayLogic(string data)
        {
            
        }
    }
}