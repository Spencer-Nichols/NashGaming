﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace NashGaming.Models
{
    public class NashGamingRepository
    {
        private NashGamingContext context;
        public NashGamingContext Context { get { return context; } }

        public NashGamingRepository()
        {
            context = new NashGamingContext();
        }

        public NashGamingRepository(NashGamingContext a_context)
        {
            context = a_context;
        }

        public List<Gamer> GetAllGamers()
        {
            var query = from gamers in context.Gamers select gamers;
            return query.ToList();
        }

        public List<Gamer> GetGamerByHandle(string handle)
        {
            var query = from gamers in context.Gamers select gamers;
            List<Gamer> found_gamers = query.Where(o => Regex.IsMatch(o.Handle, handle, RegexOptions.IgnoreCase)).ToList();
            found_gamers.Sort();
            return found_gamers;
        }
       
        public List<Team> GetAllTeams()
        {
            var query = from teams in context.Teams select teams;
            return query.ToList();
        }

        public List<Match> GetAllMatches()
        {
            var query = from matches in context.Matches select matches;
            return query.ToList();
        }

        public List<Team> SearchTeamsByName(string teamname)
        {
            var query = from team in context.Teams select team;
            List<Team> found_teams = query.Where(t => Regex.IsMatch(t.TeamName, teamname, RegexOptions.IgnoreCase)).ToList();
            found_teams.Sort();
            return found_teams;
        }

        public List<Posts> GetAllPosts()
        {
            var query = from post in context.Posts orderby post.Date descending select post;
            List<Posts> allposts = query.ToList();
            return allposts;
        }

        public List<Posts> SearchPostsByContent(string search)
        {
            var query = from post in context.Posts.Where(o => Regex.IsMatch(o.Content, search, RegexOptions.IgnoreCase)) orderby post.Date descending select post;
            List<Posts> matchingposts = query.ToList();
            return matchingposts;
        }

        public bool DeletePostById(int id)
        {
            var query = from post in context.Posts.Where(o => o.PostID == id) select post;
            Posts result = query.Single();
            try {
                context.Posts.Remove(result);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }   
        }

        public bool CreateAPost(Gamer gamer, string content)
        {
            Posts new_post = new Posts { Author = gamer, Content = content, Date = DateTime.Now };
            try
            {
                context.Posts.Add(new_post);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}