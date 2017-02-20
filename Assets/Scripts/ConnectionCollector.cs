using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionCollector : MonoBehaviour {

        public IDisposable add { set { Add(value); } }

        public void Add(IDisposable connection)
        {
            connections.Add(connection);
        }

        List<IDisposable> connections = new List<IDisposable>();

        public void DisconnectAll()
        {
            foreach (var c in connections)
            {
                c.Dispose();
            }
            connections.Clear();
        }
}
