﻿using Microsoft.EntityFrameworkCore;
using Movie.DTOs;
using Movie.Models;

namespace Movie.Services
{
    public class RendezoService
    {
        public static List<Rendezo> GetRendezok()
        {
            using(var context=new MovieContext())
            {
                try
                {
                    return context.Rendezos.ToList();
                }
                catch
                {
                    return new List<Rendezo>();
                }
            }
        }
        public static List<RendezoDTO> GetRendezoDTO()
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Rendezos.Include(r => r.Nev).Include(r => r.Nemzetiseg).Select(r => new RendezoDTO
                    {
                        Id = r.Id,
                        Nev = r.Nev,
                        Nemzetiseg = r.Nemzetiseg,
                        SzulDatum = r.SzulDatum,
                    }).ToList();
                    return response;
                }
                catch (Exception ex)
                {
                    List<RendezoDTO> response = new List<RendezoDTO>();
                    response.Add(new RendezoDTO { Id = -1, Nev = ex.Message });
                    return response;
                }
            }
        }

            public static Rendezo GetRendezo(int id)
        {
            using (var context = new MovieContext())
            {
                try
                {
                    var response = context.Rendezos.FirstOrDefault(r => r.Id == id);
                    return response;
                }
                catch
                {
                    return new Rendezo { Id = 0 };
                }
            }
        }
    }
}
