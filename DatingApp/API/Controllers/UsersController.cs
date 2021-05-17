﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using API.DTOs;
using AutoMapper;
using System.Security.Claims;

namespace API.Controllers
{
	[Authorize]
	public class UsersController : BaseApiController
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public UsersController(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}
		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
		{
			var users = await _userRepository.GetMembersAsync();
			return Ok(users);
		}

		//api/Users/2
		[HttpGet("{username}")]
		public async Task<ActionResult<MemberDto>> GetUser(string username)
		{
			var user = await _userRepository.GetMemberAsync(username);
			
			return Ok(user);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
		{
			var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var user = await _userRepository.GetUsersByUsername(username);

			_mapper.Map(memberUpdateDto, user);

			_userRepository.Update(user);

			if (await _userRepository.SaveAllAsync()) return NoContent();
			return BadRequest("Failed to update user");
		}

	}
}
