using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex07
{
    public class MinhaLista<T>
    {
        private T[] dados;
        private int tamanho;

        public MinhaLista()
        {
            dados = new T[4];
            tamanho = 0;
        }

        public MinhaLista(T[] valoresIniciais) : this()
        {
            foreach (T item in valoresIniciais) Append(item);
        }

        private void Redimensionar()
        {
            T[] novoArray = new T[dados.Length * 2];
            for (int i = 0; i < dados.Length; i++)
                novoArray[i] = dados[i];
            dados = novoArray;
        }

        public void Append(T item)
        {
            if (tamanho == dados.Length) Redimensionar();
            dados[tamanho++] = item;
        }

        public void Prepend(T item)
        {
            if (tamanho == dados.Length) Redimensionar();
            for (int i = tamanho; i > 0; i--)
                dados[i] = dados[i - 1];
            dados[0] = item;
            tamanho++;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > tamanho) throw new ArgumentOutOfRangeException();
            if (tamanho == dados.Length) Redimensionar();
            for (int i = tamanho; i > index; i--)
                dados[i] = dados[i - 1];
            dados[index] = item;
            tamanho++;
        }

        public void Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0) RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= tamanho) throw new ArgumentOutOfRangeException();
            for (int i = index; i < tamanho - 1; i++)
                dados[i] = dados[i + 1];
            tamanho--;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < tamanho; i++)
            {
                if (Equals(dados[i], item)) return i;
            }
            return -1;
        }

        public void Clear()
        {
            tamanho = 0;
        }

        public T First => tamanho > 0 ? dados[0] : default(T);
        public T Last => tamanho > 0 ? dados[tamanho - 1] : default(T);
        public int Count => tamanho;

        public T Get(int index) => dados[index];

        public override string ToString()
        {
            string resultado = "";
            for (int i = 0; i < tamanho; i++)
            {
                resultado += dados[i] + (i < tamanho - 1 ? ", " : "");
            }
            return resultado;
        }
    }

    public class Node<T>
    {
        public T Valor;
        public Node<T> Proximo;

        public Node(T valor)
        {
            Valor = valor;
            Proximo = null;
        }
    }

    public class MinhaListaEncadeada<T>
    {
        private Node<T> inicio;
        private int tamanho;

        public MinhaListaEncadeada()
        {
            inicio = null;
            tamanho = 0;
        }

        public MinhaListaEncadeada(T[] valores)
        {
            foreach (var valor in valores) Append(valor);
        }

        public void Append(T item)
        {
            Node<T> novo = new Node<T>(item);
            if (inicio == null)
            {
                inicio = novo;
            }
            else
            {
                Node<T> atual = inicio;
                while (atual.Proximo != null) atual = atual.Proximo;
                atual.Proximo = novo;
            }
            tamanho++;
        }

        public void Prepend(T item)
        {
            Node<T> novo = new Node<T>(item);
            novo.Proximo = inicio;
            inicio = novo;
            tamanho++;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > tamanho) throw new ArgumentOutOfRangeException();
            if (index == 0) { Prepend(item); return; }

            Node<T> novo = new Node<T>(item);
            Node<T> atual = inicio;
            for (int i = 0; i < index - 1; i++) atual = atual.Proximo;
            novo.Proximo = atual.Proximo;
            atual.Proximo = novo;
            tamanho++;
        }

        public void Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0) RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= tamanho) throw new ArgumentOutOfRangeException();
            if (index == 0) { inicio = inicio.Proximo; tamanho--; return; }

            Node<T> atual = inicio;
            for (int i = 0; i < index - 1; i++) atual = atual.Proximo;
            atual.Proximo = atual.Proximo.Proximo;
            tamanho--;
        }

        public bool Contains(T item) => IndexOf(item) != -1;

        public int IndexOf(T item)
        {
            int i = 0;
            Node<T> atual = inicio;
            while (atual != null)
            {
                if (Equals(atual.Valor, item)) return i;
                atual = atual.Proximo;
                i++;
            }
            return -1;
        }

        public void Clear() { inicio = null; tamanho = 0; }

        public T First => inicio != null ? inicio.Valor : default(T);

        public T Last
        {
            get
            {
                Node<T> atual = inicio;
                if (atual == null) return default(T);
                while (atual.Proximo != null) atual = atual.Proximo;
                return atual.Valor;
            }
        }

        public int Count => tamanho;

        public T Get(int index)
        {
            if (index < 0 || index >= tamanho) throw new ArgumentOutOfRangeException();
            Node<T> atual = inicio;
            for (int i = 0; i < index; i++) atual = atual.Proximo;
            return atual.Valor;
        }

        public override string ToString()
        {
            Node<T> atual = inicio;
            string resultado = "";
            while (atual != null)
            {
                resultado += atual.Valor + (atual.Proximo != null ? ", " : "");
                atual = atual.Proximo;
            }
            return resultado;
        }
    }

    public class MeuConjuntoHash<T>
    {
        private class Entrada
        {
            public T Valor;
            public bool Ocupado;

            public Entrada(T valor)
            {
                Valor = valor;
                Ocupado = true;
            }
        }

        private Entrada[] tabela;
        private int tamanho;

        public MeuConjuntoHash()
        {
            tabela = new Entrada[16];
            tamanho = 0;
        }

        public MeuConjuntoHash(T[] valoresIniciais) : this()
        {
            foreach (var item in valoresIniciais)
            {
                Append(item);
            }
        }

        private int Hash(T item)
        {
            return (item.GetHashCode() & 0x7FFFFFFF) % tabela.Length;
        }

        private void Redimensionar()
        {
            var antigo = tabela;
            tabela = new Entrada[antigo.Length * 2];
            tamanho = 0;

            foreach (var entrada in antigo)
            {
                if (entrada != null && entrada.Ocupado)
                {
                    Append(entrada.Valor);
                }
            }
        }

        public void Append(T item)
        {
            if (Contains(item)) return;
            if (tamanho >= tabela.Length / 2) Redimensionar();

            int pos = Hash(item);
            while (tabela[pos] != null && tabela[pos].Ocupado)
            {
                pos = (pos + 1) % tabela.Length;
            }
            tabela[pos] = new Entrada(item);
            tamanho++;
        }

        public void Prepend(T item) => Append(item);
        public void Insert(int index, T item) => Append(item);

        public void Remove(T item)
        {
            int pos = Hash(item);
            while (tabela[pos] != null)
            {
                if (tabela[pos].Ocupado && Equals(tabela[pos].Valor, item))
                {
                    tabela[pos].Ocupado = false;
                    tamanho--;
                    return;
                }
                pos = (pos + 1) % tabela.Length;
            }
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException("ConjuntoHash não suporta acesso por índice.");
        }

        public bool Contains(T item)
        {
            int pos = Hash(item);
            while (tabela[pos] != null)
            {
                if (tabela[pos].Ocupado && Equals(tabela[pos].Valor, item)) return true;
                pos = (pos + 1) % tabela.Length;
            }
            return false;
        }

        public int IndexOf(T item)
        {
            throw new NotSupportedException("ConjuntoHash não suporta índice.");
        }

        public void Clear()
        {
            tabela = new Entrada[16];
            tamanho = 0;
        }

        public T First
        {
            get
            {
                foreach (var entrada in tabela)
                {
                    if (entrada != null && entrada.Ocupado) return entrada.Valor;
                }
                return default(T);
            }
        }

        public T Last
        {
            get
            {
                T ultimo = default(T);
                foreach (var entrada in tabela)
                {
                    if (entrada != null && entrada.Ocupado) ultimo = entrada.Valor;
                }
                return ultimo;
            }
        }

        public int Count => tamanho;

        public override string ToString()
        {
            List<T> lista = new List<T>();
            foreach (var entrada in tabela)
            {
                if (entrada != null && entrada.Ocupado) lista.Add(entrada.Valor);
            }
            return string.Join(", ", lista.ToArray());
        }
    }

    public class MeuMapaHash<TKey, TValue>
    {
        private class Entrada
        {
            public TKey Chave;
            public TValue Valor;
            public bool Ocupado;

            public Entrada(TKey chave, TValue valor)
            {
                Chave = chave;
                Valor = valor;
                Ocupado = true;
            }
        }

        private Entrada[] tabela;
        private int tamanho;

        public MeuMapaHash()
        {
            tabela = new Entrada[16];
            tamanho = 0;
        }

        public MeuMapaHash(TKey[] chaves, TValue[] valores) : this()
        {
            for (int i = 0; i < chaves.Length; i++)
            {
                Append(chaves[i], valores[i]);
            }
        }

        private int Hash(TKey chave)
        {
            return (chave.GetHashCode() & 0x7FFFFFFF) % tabela.Length;
        }

        private void Redimensionar()
        {
            var antigo = tabela;
            tabela = new Entrada[antigo.Length * 2];
            tamanho = 0;

            foreach (var entrada in antigo)
            {
                if (entrada != null && entrada.Ocupado)
                {
                    Append(entrada.Chave, entrada.Valor);
                }
            }
        }

        public void Append(TKey chave, TValue valor)
        {
            if (tamanho >= tabela.Length / 2) Redimensionar();

            int pos = Hash(chave);
            while (tabela[pos] != null && tabela[pos].Ocupado)
            {
                if (Equals(tabela[pos].Chave, chave))
                {
                    tabela[pos].Valor = valor;
                    return;
                }
                pos = (pos + 1) % tabela.Length;
            }
            tabela[pos] = new Entrada(chave, valor);
            tamanho++;
        }

        public void Prepend(TKey chave, TValue valor) => Append(chave, valor);
        public void Insert(int index, KeyValuePair<TKey, TValue> item) => Append(item.Key, item.Value);

        public void Remove(TKey chave)
        {
            int pos = Hash(chave);
            while (tabela[pos] != null)
            {
                if (tabela[pos].Ocupado && Equals(tabela[pos].Chave, chave))
                {
                    tabela[pos].Ocupado = false;
                    tamanho--;
                    return;
                }
                pos = (pos + 1) % tabela.Length;
            }
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException("MapaHash não suporta índice.");
        }

        public bool Contains(TKey chave)
        {
            int pos = Hash(chave);
            while (tabela[pos] != null)
            {
                if (tabela[pos].Ocupado && Equals(tabela[pos].Chave, chave)) return true;
                pos = (pos + 1) % tabela.Length;
            }
            return false;
        }

        public TValue Get(TKey chave)
        {
            int pos = Hash(chave);
            while (tabela[pos] != null)
            {
                if (tabela[pos].Ocupado && Equals(tabela[pos].Chave, chave)) return tabela[pos].Valor;
                pos = (pos + 1) % tabela.Length;
            }
            return default(TValue);
        }

        public int IndexOf(TKey chave)
        {
            throw new NotSupportedException("MapaHash não suporta índice.");
        }

        public void Clear()
        {
            tabela = new Entrada[16];
            tamanho = 0;
        }

        public TValue First
        {
            get
            {
                foreach (var entrada in tabela)
                {
                    if (entrada != null && entrada.Ocupado) return entrada.Valor;
                }
                return default(TValue);
            }
        }

        public TValue Last
        {
            get
            {
                TValue ultimo = default(TValue);
                foreach (var entrada in tabela)
                {
                    if (entrada != null && entrada.Ocupado) ultimo = entrada.Valor;
                }
                return ultimo;
            }
        }

        public int Count => tamanho;

        public override string ToString()
        {
            List<string> itens = new List<string>();
            foreach (var entrada in tabela)
            {
                if (entrada != null && entrada.Ocupado)
                    itens.Add(entrada.Chave + ": " + entrada.Valor);
            }
            return string.Join(", ", itens.ToArray());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Teste: MinhaLista<int> ===");
            var lista = new MinhaLista<int>();
            lista.Append(10);
            lista.Append(20);
            lista.Prepend(5);
            lista.Insert(1, 15);
            Console.WriteLine("Lista: " + lista);
            Console.WriteLine("Contém 15? " + lista.Contains(15));
            Console.WriteLine("Índice do 20: " + lista.IndexOf(20));
            Console.WriteLine("Primeiro: " + lista.First + ", Último: " + lista.Last);
            lista.Remove(15);
            lista.RemoveAt(0);
            Console.WriteLine("Após remoções: " + lista);
            lista.Clear();
            Console.WriteLine("Após clear: " + lista + " (Count: " + lista.Count + ")");

            Console.WriteLine("\n=== Teste: MinhaListaEncadeada<string> ===");
            var encadeada = new MinhaListaEncadeada<string>();
            encadeada.Append("B");
            encadeada.Prepend("A");
            encadeada.Append("C");
            encadeada.Insert(1, "AB");
            Console.WriteLine("Encadeada: " + encadeada);
            Console.WriteLine("Contém 'C'? " + encadeada.Contains("C"));
            Console.WriteLine("Índice do 'B': " + encadeada.IndexOf("B"));
            Console.WriteLine("Primeiro: " + encadeada.First + ", Último: " + encadeada.Last);
            encadeada.Remove("AB");
            encadeada.RemoveAt(1);
            Console.WriteLine("Após remoções: " + encadeada);
            encadeada.Clear();
            Console.WriteLine("Após clear: " + encadeada + " (Count: " + encadeada.Count + ")");

            Console.WriteLine("\n=== Teste: MeuConjuntoHash<char> ===");
            var conjunto = new MeuConjuntoHash<char>();
            conjunto.Append('A');
            conjunto.Append('B');
            conjunto.Append('A'); // duplicata ignorada
            conjunto.Append('C');
            Console.WriteLine("Conjunto: " + conjunto);
            Console.WriteLine("Contém 'B'? " + conjunto.Contains('B'));
            conjunto.Remove('B');
            Console.WriteLine("Após remover 'B': " + conjunto);
            conjunto.Clear();
            Console.WriteLine("Após clear: " + conjunto + " (Count: " + conjunto.Count + ")");

            Console.WriteLine("\n=== Teste: MeuMapaHash<string, int> ===");
            var mapa = new MeuMapaHash<string, int>();
            mapa.Append("um", 1);
            mapa.Append("dois", 2);
            mapa.Append("três", 3);
            mapa.Append("dois", 22); // substitui valor
            Console.WriteLine("Mapa: " + mapa);
            Console.WriteLine("Valor de 'dois': " + mapa.Get("dois"));
            Console.WriteLine("Contém chave 'três'? " + mapa.Contains("três"));
            mapa.Remove("um");
            Console.WriteLine("Após remover 'um': " + mapa);
            mapa.Clear();
            Console.WriteLine("Após clear: " + mapa + " (Count: " + mapa.Count + ")");
        }
    }
}
