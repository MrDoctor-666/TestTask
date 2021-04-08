using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Company
    {
        int n = 0; //departments
        int m; //stamps
        List<Department> dep = new List<Department>();
        VasyaTravel lastTravel;
        //in case we will give the ability to change departments
        //we could keep a list of travels
        public Company(int n, int m)
        {
            this.n = n;
            this.m = m;
        }

        public void CreateDepartment(bool type, int i, int j, int k_id, int s = 0, int t = 0, int r = 0, int p_id = 0)
        {
            if (dep.Count == n) throw new Exception("You have enough Departments"); //could add stuff to increase n?
            if (i > m || j > m || s > m || t > m || r > m) throw new Exception("Wrong stamp number");
            if (k_id > n || p_id > n) throw new Exception("Wrong department number");
            Department newD = new Department(dep.Count, type, i, j, k_id, s, t, r, p_id);
            dep.Add(newD);
        }
        public bool StartDetour(int start_id, int end_id)
        {
            //if returns true - it's endless
            if (dep.Count != n) throw new Exception("Some departments don't exist");
            VasyaTravel travel = new VasyaTravel(dep[start_id - 1], dep[end_id - 1], dep);
            lastTravel = travel;
            return travel.IsEndless; 
        }
        public bool WasVisited(int dep_id, out List<List<int>> states)
        {
            //if we want to get all of the states
            List<States> st;
            states = new List<List<int>>(); //list of lists of stamps
            bool a =  lastTravel.WasVisited(dep[dep_id - 1], out st);
            foreach (States state in st)
            {
                states.Add(state.Stamps);
            }
            return a;
        }
        public bool WasVisited(int dep_id, out List<int> states)
        {
            //if we want to get only one state
            List<States> st;
            states = new List<int>();
            bool a = lastTravel.WasVisited(dep[dep_id - 1], out st);
            if (a == false) return a;
            foreach (States state in st)
            {
                states = state.Stamps;
            }
            return a;
        }



        class VasyaTravel
        {
            List<int> stamps;
            List<Department> Dep;
            // List<int> crossedStamps; //needed??
            List<States> visited;
            Department end;
            int depVisited;
            int n;
            bool isEndless = false;

            public VasyaTravel(Department start, Department end, List<Department> Dep)
            {
                stamps = new List<int>();
                //crossedStamps = new List<int>();
                visited = new List<States>();
                this.end = end;
                this.Dep = Dep;
                this.n = Dep.Count;
                depVisited = 0;
                Go(start);
            }

            public void Go(Department hereNow)
            {
                if (depVisited > Math.Pow(n, 3))
                {
                    //assume it's an endless cycle
                    //get some rest for vanya
                    isEndless = true;
                    return;
                }
                depVisited++;
                if ((hereNow.Type && stamps.Contains(hereNow.S)) || !hereNow.Type)
                {
                    //conditional and stamp S exist (then we use i, j marks and go to k department)
                    //or it's unconditional
                    if (!stamps.Contains(hereNow.I)) stamps.Add(hereNow.I);
                    if (stamps.Contains(hereNow.J)) { stamps.Remove(hereNow.J);/* crossedStamps.Remove(hereNow.J);*/ }
                    //saving status for now
                    List<int> stampsCur = new List<int>(stamps);
                    States current = new States(hereNow, stampsCur);
                    visited.Add(current);
                    //if it's the last place
                    if (hereNow == end) return;
                    Go(Dep[hereNow.K-1]);
                }
                else
                {
                    //only works for conditional departments
                    if (!stamps.Contains(hereNow.T)) stamps.Add(hereNow.T);
                    if (stamps.Contains(hereNow.R)) { stamps.Remove(hereNow.R); /*crossedStamps.Remove(hereNow.R);*/ }
                    //saving status for now
                    List<int> stampsCur = new List<int>(stamps);
                    States current = new States(hereNow, stampsCur);
                    visited.Add(current);
                    if (hereNow == end) return;
                    Go(Dep[hereNow.P - 1]);
                }
            }

            public bool WasVisited(Department Q, out List<States> st)
            {
                st = new List<States>();
                foreach (States state in visited)
                {
                    if (state.Exited == Q) st.Add(state);
                }

                if (st.Count == 0) return false;
                else return true;
            }

            public bool IsEndless
            {
                get { return isEndless; }
            }
        }
        class States
        {
            Department exited;
            List<int> stamps;

            public States(Department exited, List<int> stamps)
            {
                this.exited = exited;
                this.stamps = stamps;
            }
            public Department Exited
            {
                get { return exited; }
            }

            public List<int> Stamps
            {
                get { return stamps; }
            }
        }
        class Department
        {
             //??
            int id;

            bool type; //0 - unconditional, 1 - conditional
            int i;
            int j;
            int k_id; //department to go

            int s; //condition
            int t;
            int r;
            int p_id; //department to go

            public Department(int depCount, bool type, int i, int j, int k_id, int s, int t, int r, int p_id)
            {
                this.type = type;
                this.i = i; this.j = j;
                this.k_id = k_id;
                this.id = depCount + 1;
                //consider stamp == 0 non-existent
                if (!type) { s = 0; t = 0; r = 0; p_id = 0; }
                else { this.s = s; this.t = t; this.r = r; this.p_id = p_id; }
            }

            public bool Type
            {
                get { return type; }
            }
            public int S
            {
                get { return s; }
            }
            public int I
            {
                get { return i; }
            }
            public int J
            {
                get { return j; }
            }
            public int T
            {
                get { return t; }
            }
            public int R
            {
                get { return r; }
            }
            public int K
            {
                get { return k_id; }
            }
            public int P
            {
                get { return p_id; }
            }
        }

    }
}
