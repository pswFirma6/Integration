﻿using Integration_library.Pharmacy.IRepository;
using Integration_library.Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration_library.Pharmacy.Repository
{
    public class OfferRepository : Repo<Model.Offer>, IOfferRepository
    {
        public OfferRepository(DatabaseContext context) : base(context)
        {
        }
    }
    }