using Barometr.Infrastructure;
using Barometr.Models;
using Barometr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barometr.Services
{
    public class ProfileService
    {
        private ProfileRepository _repo;
        public ProfileService(ProfileRepository repo)
        {
            _repo = repo;
        }

        public IList<ProfileDTO> GetProfileDTO()
        {
            return (from p in _repo.List()
                    select new ProfileDTO
                    {
                        Id = p.Id,
                        DOB = p.DOB,
                        Faction = p.Faction,
                        Location = p.Location,
                        Name = p.Name
                    }).ToList();
        }
        public ProfileDTO GetProfileById(int id)
        {
            var profile = _repo.List().Where(p => p.Id == id).Select(p => new ProfileDTO
            {
                Id = p.Id,
                DOB = p.DOB,
                Faction = p.Faction,
                Location = p.Location,
                Name = p.Name

            }).FirstOrDefault();

            return profile;
        }

        public ProfileDTO GetProfileByEmail(string user)
        {
            var profileUser = _repo.GetProfiles().Where(p => p.User.Email == user).Select(p => new ProfileDTO
            {
                Id = p.Id,
                DOB = p.DOB,
                Faction = p.Faction,
                Location = p.Location,
                Name = p.Name
            }).FirstOrDefault();
            
            return profileUser;
        }
        public void AddProfile(ProfileDTO profiledto, string username)
        {
            var user = _repo.GetUserByUsername(username);
            var profile = new Profile
            {
                Id = profiledto.Id,
                DOB = profiledto.DOB,
                Faction = profiledto.Faction,
                Location = profiledto.Location,
                Name = profiledto.Name

            };
            _repo.Add(profile);
            _repo.SaveChanges();
        }

        //update method
        public void UpdateProfile(ProfileDTO profile)
        {
            var orig = _repo.GetProfileById(profile.Id);
            orig.Location = profile.Location;
            orig.Faction = profile.Faction;
            _repo.SaveChanges();

        }



        //delete method
        //public void DeleteProfile(int id)
        //{
        //    var orig = _repo.GetProfileById(id);
        //    _repo.Delete(orig);
        //    _repo.SaveChanges();
        //}

    }
}
