﻿using netbullAPI.Entidade;
using netbullAPI.Persistencia;

namespace netbullAPI.Negocio
{
    public class NE_Item
    {
        DAOItem daoItem;
        public NE_Item(DAOItem daoItem)
        {
            this.daoItem = daoItem;
        }
        public IEnumerable<Item> BuscaItemPedido(int id)
        {
            return daoItem.BuscaItemPedido(id);
        }
        public Item AdicionaItem(Item item)
        {
            return daoItem.AdicionaItem(item);
        }
        public bool DeletaItem(int id)
        {
            return daoItem.DeletaItem(id);
        }
        public bool AlteraQuantidadeProduto(Item item, int quantidade)
        {
            return daoItem.AlteraQuantidadeProduto(item, quantidade);
        }
    }
}
