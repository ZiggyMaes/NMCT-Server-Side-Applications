//using Oef2.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Oef_2
//{
//    public class Data
//    {
//        private static List<Session> sessions = new List<Session>();
//        private static List<Organization> organizations = new List<Organization>();
//        private static List<Device> devices = new List<Device>();

//        static Data()
//        {
//            sessions.Add(new Session() { Id = 1, Slot = 1, Name = "Scaling a video site using Microsoft Azure", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });
//            sessions.Add(new Session() { Id = 2, Slot = 1, Name = "Debugging Amazon EC2: tips & tricks from a pro", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });
//            sessions.Add(new Session() { Id = 3, Slot = 1, Name = "Developing drone software for nuclear attacks", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });
//            sessions.Add(new Session() { Id = 4, Slot = 2, Name = "Programming Internet of Things", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });
//            sessions.Add(new Session() { Id = 5, Slot = 2, Name = "Arduino Hacking", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });
//            sessions.Add(new Session() { Id = 6, Slot = 2, Name = "Android for C# Developers", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });
//            sessions.Add(new Session() { Id = 7, Slot = 3, Name = "Big Data analysis for senior financial managers", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });
//            sessions.Add(new Session() { Id = 8, Slot = 3, Name = "Hadoop for dummies", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });
//            sessions.Add(new Session() { Id = 9, Slot = 3, Name = "Windows for devices: a practical guide", Description = "El snort testosterone trophy driving gloves handsome, dis el snort handsome gent testosterone trophy Fallen eyebrow driving gloves cardinal richelieu gentleman face broom, chevron driving gloves dis cardinal richelieu gentleman gent el snort handsome ron burgundy Leonine funny walk groucho marx Fallen eyebrow rock n roll star great dictator testosterone trophy face broom?" });

//            organizations.Add(new Organization() { Id = 1, Name = "Howest" });
//            organizations.Add(new Organization() { Id = 2, Name = "Vives" });
//            organizations.Add(new Organization() { Id = 3, Name = "HoGent" });
//            organizations.Add(new Organization() { Id = 4, Name = "HoLimburg" });
//            organizations.Add(new Organization() { Id = 4, Name = "De blauwe smurfen" });

//            devices.Add(new Device() { Id = 1, Name = "Laptop" });
//            devices.Add(new Device() { Id = 2, Name = "Tablet" });
//            devices.Add(new Device() { Id = 3, Name = "Apple Watch" });
//        }

//        public static List<Device> GetDevices()
//        {
//            return devices;
//        }

//        public static List<Organization> GetOrganizations()
//        {
//            return organizations;
//        }

//        public static Organization GetOrganization(int id)
//        {
//            return organizations.Find(o => o.Id == id);
//        }

//        public static Session GetSession(int id)
//        {
//            return sessions.Find(o => o.Id == id);
//        }

//        public static List<Session> GetSessions(int slot)
//        {
//            return sessions.Where(s => s.Slot == slot).ToList<Session>();

//        }

//        public static Session FindSession(int p)
//        {
//            return sessions.Where(s => s.Id == p).SingleOrDefault<Session>();
//        }
//    }
//}