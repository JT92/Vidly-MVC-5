﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Models;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        #region DB Context

        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        #endregion
        
        // GET /api/movies
        public IHttpActionResult GetMovies(string query = null)
        {


            // eager load "moviesDto + Genre" (uses Entity)
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            var moviesDto = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie,MovieDto>);

            return Ok(moviesDto);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            // Get the movie or return error
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            // return success msg
            var movieDto = Mapper.Map<Movie, MovieDto>(movie);
            return Ok(movieDto);
        }

        // POST /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            // validate request
            if (!ModelState.IsValid)
                return BadRequest();

            // create movie
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            // return msg
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(MovieDto movieDto, int id)
        {
            // validate request
            if (!ModelState.IsValid)
                return BadRequest();

            // get movie from db
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            // update movie
            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();

            // return msg
            movieDto.Id = movieInDb.Id;
            return Ok(movieDto);
        }

        // DELETE /api/movies/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            // validate request
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            
            // find movie
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // delete movie
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            // return msg
            return Ok();
        }

    }
}
