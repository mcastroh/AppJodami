﻿using System.Linq.Expressions;

namespace Jodami.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Obtener(Expression<Func<T, bool>> filtro);

        Task<T> Crear(T entidad);

        Task<bool> Editar(T entidad);

        Task<bool> Eliminar(T entidad);

        Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro = null);

    }
}
