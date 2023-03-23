using System;

namespace BookConnecion.Repository
{
    internal class RouteAttribute : Attribute
    {
        private string v;

        public RouteAttribute(string v)
        {
            this.v = v;
        }
    }
}