﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();

        IDataResult<Rental> GetById(int rentalId);

        IResult Add(Rental rental);

        IResult Update(Rental rental);

        IResult Delete(Rental rental);
    }
}
