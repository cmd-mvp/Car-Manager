using Car_Manager.Data;
using Car_Manager.Models;
using Microsoft.AspNetCore.Mvc;
namespace Car_Manager.Services;
public class RentServices
{
    private CarContext _carContext;
    private UserContext _userContext;

    public RentServices(CarContext carContext, UserContext userContext)
    {
        _carContext = carContext;
        _userContext = userContext;
    }
    public async Task<ActionResult<Rent>> PostRent(Rent rent)
    {
        var car = _carContext.Cars.Where(u => u.Id == rent.CarId).FirstOrDefault();
        var user = _userContext.Users.Where(u => u.CPF == rent.UserName).FirstOrDefault();

        if (car == null && user == null) return null!;

        var check = CheckCarAvaiable(car.Id);

        if (check == null) return null;

        car.Available = false;
        rent.Status = Status.Pending.ToString().ToUpper();
        rent.RentDate = DateTime.Now;
        rent.ReturnDate = DateTime.Now.AddDays(1);

        _carContext.Rents.Add(rent);
        return rent;
    }
    public async Task<Rent> GetRent(int id)
    {
        var rent = _carContext.Rents.FirstOrDefault(u=>u.Id == id);
        return rent!;
    }
    public async Task<Rent> ReturnRent(int id)
    {
        var rent = _carContext.Rents.Where(c => c.Id == id).FirstOrDefault();
        if (rent is null) return null;
        rent.Status = Status.Finished.ToString().ToUpper();
        var car = _carContext.Cars.Where(c => c.Id == rent.CarId).FirstOrDefault();
        car!.Available = true;
        _carContext.Remove(rent);
        _carContext.SaveChanges();
        return rent;
    }
    private object CheckCarAvaiable(int carId)
    {
        Car car = _carContext.Cars.FirstOrDefault(c => c.Id == carId)!;
        bool disponivel = true;
        if (car.Available != disponivel)
        {
            return null;
        }
        return car;
    }
    internal object UpdateStatus()
    {
        var rents = _carContext.Rents.ToList();
        foreach (var item in rents)
        {
            var span = DateTime.Compare(item!.ReturnDate, DateTime.Now);
            if (span <= 0)
            {
                item.Status = Status.Late.ToString().ToUpper();
                _carContext.SaveChanges();
                return item;
            }
        }return rents;
    }
    internal object CheckAndUpdateStatus(string status)
    {
        UpdateStatus();
        var rent = _carContext.Rents.ToList().Where(c => c.Status.Equals(status));
        return rent;
    }
}
public enum Status { Pending, Late, Finished }
