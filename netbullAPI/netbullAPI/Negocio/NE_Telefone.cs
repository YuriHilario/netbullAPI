﻿using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace netbullAPI.Negocio
{
    public class NE_Telefone
    {
        private DAOTelefone _daoTelefone;
        public NE_Telefone(DAOTelefone daoTelefone)
        {
            _daoTelefone = daoTelefone;
        }

        public IEnumerable<Telefone>  BuscaTelefoneCliente(int id)
        {
            return _daoTelefone.BuscaTelefoneCliente(id);            
        }

        public Telefone AdicionaTelefone(Telefone telefone)
        {
            return _daoTelefone.AdicionaTelefone(telefone);
        }

        public bool AtualizaTelefone(Telefone telefone)
        {
            return _daoTelefone.AtualizaTelefone(telefone);
        }

        public bool DeletaTelefone(int id)
        {
            return _daoTelefone.DeletaTelefone(id);
        }
    }
}
