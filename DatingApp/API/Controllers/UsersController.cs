﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly DataContext _context;
		public UsersController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}

		//api/Users/2
		[HttpGet("{id}")]
		public async Task<ActionResult<AppUser>> GetUser(int id)
		{
			return await _context.Users.FindAsync(id);
		}
	}
}
