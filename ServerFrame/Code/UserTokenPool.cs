using System.Collections.Generic;

namespace ServerFrame.Code
{
    public class UserTokenPool
    {
        private Stack<UserToken> pool;

        public UserTokenPool(int max)
        {
            pool = new Stack<UserToken>(max);
        }

        public UserToken Pop()
        {
            UserToken user = pool.Pop();
            if (user != null)
            {
                return user;
            }
            user = new UserToken();
            return user;
        }

        public void Push(UserToken user)
        {
            if (user != null)
            {
                pool.Push(user);
            }
        }

        public int Size
        {
            get { return pool.Count; }
        }
    }
}