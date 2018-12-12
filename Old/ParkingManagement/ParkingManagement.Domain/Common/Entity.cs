namespace ParkingManagement.Domain

{
    using System;

    public abstract class Entity
    {
        protected Entity()
        {
            Id = new Guid("1B29DC6C-08BA-495F-8F57-58C7B2A80E75");
        }

        public Guid Id { get; private set; }
    }
}