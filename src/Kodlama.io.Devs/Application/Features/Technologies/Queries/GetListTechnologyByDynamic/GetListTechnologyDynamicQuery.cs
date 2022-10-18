﻿using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetListTechnologyDynamic
{
    public class GetListTechnologyDynamicQuery
    {
        public class GetListTechnologyByDynamicQuery : IRequest<TechnologyListModel>
        {
            public Dynamic Dynamic { get; set; }
            public PageRequest PageRequest { get; set; }

            public class GetListTechnologyByDynamicQueryHandler : IRequestHandler<GetListTechnologyByDynamicQuery, TechnologyListModel>
            {
                private readonly IMapper _mapper;
                private readonly ITechnologyRepository _technologyRepository;

                public GetListTechnologyByDynamicQueryHandler(IMapper mapper, ITechnologyRepository technologyRepository)
                {
                    _mapper = mapper;
                    _technologyRepository = technologyRepository;
                }

                public async Task<TechnologyListModel> Handle(GetListTechnologyByDynamicQuery request, CancellationToken cancellationToken)
                {
                    //join işlemi
                    IPaginate<Technology> models = await _technologyRepository.GetListByDynamicAsync(request.Dynamic,
                                                  include:
                                                  m => m.Include(c => c.ProgrammingLanguage),
                                                  index: request.PageRequest.Page,
                                                  size: request.PageRequest.PageSize
                                                  );
                    TechnologyListModel technology = _mapper.Map<TechnologyListModel>(models);
                    return technology;
                }
            }
        }
    }
}
