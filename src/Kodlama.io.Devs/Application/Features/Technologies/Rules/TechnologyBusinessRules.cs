﻿using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task TechnologyCannotBeDuplicatedWhenInsterted(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology exists.");
        }

        public void TechnologyExistWhenRequested(Technology technology)
        {
            if (technology == null) throw new BusinessException("Requested technology does not exist.");
        }
    }
}
