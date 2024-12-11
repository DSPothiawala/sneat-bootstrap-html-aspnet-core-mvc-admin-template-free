﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using AspnetCoreMvcFull.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Models
{
    public partial class CoreDataContext
    {
        private ICoreDataContextProcedures _procedures;

        public virtual ICoreDataContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new CoreDataContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public ICoreDataContextProcedures GetProcedures()
        {
            return Procedures;
        }
    }

    public partial class CoreDataContextProcedures : ICoreDataContextProcedures
    {
        private readonly CoreDataContext _context;

        public CoreDataContextProcedures(CoreDataContext context)
        {
            _context = context;
        }

        public virtual async Task<List<GetMenuItemResult>> GetMenuItemAsync(int? userid, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "userid",
                    Value = userid ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<GetMenuItemResult>("EXEC @returnValue = [dbo].[GetMenuItem] @userid = @userid", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<GetSubMenuItemResult>> GetSubMenuItemAsync(int? userid, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "userid",
                    Value = userid ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<GetSubMenuItemResult>("EXEC @returnValue = [dbo].[GetSubMenuItem] @userid = @userid", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
