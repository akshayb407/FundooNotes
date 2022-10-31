using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        ICollabRL collabRl;
        public CollabBL(ICollabRL collabRl)
        {
            this.collabRl = collabRl;
        }
        public CollabEntity AddCollab(long noteid, long userid, string email)
        {
            try
            {
                return this.collabRl.AddCollab(noteid, userid, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Remove(long collabid)
        {
            try
            {
                return this.collabRl.Remove(collabid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CollabEntity> GetAllByNoteID(long noteid)
        {
            try
            {
                return this.collabRl.GetAllByNoteID(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
