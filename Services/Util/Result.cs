using System;

namespace Services.Util
{
    /// <summary>
    /// Classe para retornos de validação dos serviços
    /// </summary>
    /// <typeparam name="E">Tipo de Erro</typeparam>
    /// <typeparam name="T">Tipo de retorno</typeparam>
    public class Result<E, T>
    {
        private readonly T t;
        private readonly E e;

        /// <summary>
        /// Retorna true caso haja erro de validação
        /// </summary>
        public bool IsError => e != null;

        /// <summary>
        /// Construtor de retorno (sem erros)
        /// </summary>
        /// <param name="t"></param>
        public Result(T t)
        {
            this.t = t;
        }

        /// <summary>
        /// Construtor de erro
        /// </summary>
        /// <param name="e"></param>
        public Result(E e)
        {
            this.e = e;
        }

        /// <summary>
        /// Função para acesso seguro do valor interno da classe
        /// </summary>
        /// <typeparam name="C">Tipo de retorno</typeparam>
        /// <param name="err">função a ser aplicada caso haja erro</param>
        /// <param name="ok">função a ser aplicada caso haja resultado</param>
        /// <returns></returns>
        public C Either<C>(Func<E,C> err, Func<T,C> ok) => IsError ? err(e) : ok(t);
    }
}
