namespace CaculateShipFeeUsageDelegate
{
    // declare the delegate type used to calculate the fees
    public delegate void ShippingFeesDelegate(decimal price, ref decimal fee);

    // This is a base class that is used as a foundation
    // for each of the destination zones
    internal abstract class ShippingDestination
    {
        public bool isHighRisk;

        public virtual void CalcFees(decimal price, ref decimal fee)
        {
        }

        // This static method returns an actual ShippingDestination object given the name of the destination, or null if none exists
        public static ShippingDestination GetDestinationInfo(string dest)
        {
            if (dest.Equals("HaNoi"))
            {
                return new HaNoiZone();
            }
            if (dest.Equals("HoChiMinh"))
            {
                return new HoChiMinhZone();
            }
            if (dest.Equals("DaNang"))
            {
                return new DaNangZone();
            }
            return null;
        }

        // Now we define implementation classes for each of the real shipping destinations. We can add as many as we like as the need arises
        private class HaNoiZone : ShippingDestination
        {
            public HaNoiZone()
            {
                this.isHighRisk = false;
            }

            public override void CalcFees(decimal price, ref decimal fee)
            {
                // charge 25%
                fee = price * 0.25m;
            }
        }

        private class HoChiMinhZone : ShippingDestination
        {
            public HoChiMinhZone()
            {
                this.isHighRisk = false;
            }

            public override void CalcFees(decimal price, ref decimal fee)
            {
                // charge 10%
                fee = price * 0.1m;
            }
        }

        private class DaNangZone : ShippingDestination
        {
            public DaNangZone()
            {
                this.isHighRisk = true;
            }

            public override void CalcFees(decimal price, ref decimal fee)
            {
                // charge 40%
                fee = price * 0.40m;
            }
        }
    }
}