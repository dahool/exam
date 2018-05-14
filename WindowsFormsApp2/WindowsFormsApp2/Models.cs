using System;
using System.Collections.Generic;

namespace service
{
    using models;

    public class BuildingFactory
    {

        private BuildingFactory()
        {
        }

        public static Building create()
        {
            Owner o1 = new Owner("juan","teste");
            Owner o2 = new Owner("jose","pato");
            Owner o3 = new Owner("jose", "ganso");

            Building b = Building.create();
            b.addUnit(o1);
            b.addUnit(o2);
            b.addUnit(o3);
            return b;
        }

    }
}

namespace models
{

    public delegate void OwnerFoundEventHandler(object sender, Owner o);

    public delegate void ResultNotificationEventHandler(object sender, Owner o);

    public class Building
    {
        private List<Unit> units = new List<Unit>();

        public ResultNotificationEventHandler ResultNotificationEvent;

        private Building()
        {
        }

        public void search(String name)
        {
            foreach (Unit u in units)
            {
                u.search(name);
            }
        }

        public void addUnit(Owner o)
        {
            Unit u = new Unit(this, o);
            this.units.Add(u);
        }

        public static Building create()
        {
            return new Building();
        }

        public void OnFoundNotification(object sender, Owner o)
        {
            if (ResultNotificationEvent != null)
            {
                ResultNotificationEvent(this, o);
            }
        }
    }

    public class Unit
    {

        private List<Owner> owners = new List<Owner>();

        private Building building;

        public Unit(Building b, Owner o)
        {
            this.building = b;
            addOwner(o);
        }

        public void addOwner(Owner o)
        {
            o.OwnerFoundEvent += this.building.OnFoundNotification;
            this.owners.Add(o);
        }

        public void search(String name)
        {
            foreach (Owner o in owners)
            {
                o.validate(name);
            }
        }

    }

    public class Owner
    {
        public String name;

        public String lastName;

        public event OwnerFoundEventHandler OwnerFoundEvent;

        public Owner(String name, String lastName)
        {
            this.name = name;
            this.lastName = lastName;
        }

        public void validate(String name)
        {
            if (this.name == name)
            {
                onOwnerFound(this);
            }
        }

        public void onOwnerFound(Owner o)
        {
            if (OwnerFoundEvent != null)
            {
                OwnerFoundEvent(this, o);
            }
        }

    }

}

