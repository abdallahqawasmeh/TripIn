﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FlightTracker.Core.Common;

namespace FlightTracker.Infra.Common
{
	public class DbContext : IDbContext
	{

		private DbConnection _connection;

		private readonly IConfiguration _configuration;



		public DbContext(IConfiguration configuration)
		{
			_configuration = configuration;

		}

		public DbConnection Connection
		{
			get
			{

				if (_connection == null)
				{
					_connection = new OracleConnection(_configuration["ConnectionStrings:FlightTracker"]);
					_connection.Open();
				}
				else if (_connection.State != ConnectionState.Open)
				{
					_connection.Open();
				}

				return _connection;


			}






		}


	}
}