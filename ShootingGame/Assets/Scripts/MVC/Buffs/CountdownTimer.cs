using System.Diagnostics;
using System.Threading;
using UnityEngine.Events;
using System;

namespace Model.ShootingGame
{
    public sealed class CountdownTimer: IDisposable // ����������������� ������ �� �������, ������������� ���� �� ������� ���������� �� MVC, ����� ������ ������ �� ����.
                                               // �� ������ ��������� ����������, ����� �� ��� ������ ��� ���������� �������� ��������� �������, �� ��������� ������ �� �����?
    {
        public UnityAction timeIsOver;
        private float _duration;
        private Thread _thread;
        public CountdownTimer(int duration)
        {
            _duration = (float)duration;
            _thread = new Thread(StartTimer); 
            _thread.Start();
        }

        public CountdownTimer(float duration)
        {
            _duration = duration;
            _thread = new Thread(StartTimer);
            _thread.Start();
        }

        private void StartTimer()
        {
            DateTime startTime = DateTime.Now;
            double watchingTime = 0;

            while (_duration - watchingTime >= 0)
            {
                watchingTime = (DateTime.Now - startTime).TotalSeconds;
            }
            timeIsOver?.Invoke();
        }

        public void Dispose()
        {
            _thread.Abort();
            _thread.Join();
        }
    }
}
