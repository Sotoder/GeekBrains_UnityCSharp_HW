using System.Diagnostics;
using System.Threading;
using UnityEngine.Events;
using System;

namespace Model.ShootingGame
{
    public sealed class BuffTimer: IDisposable // ����������������� ������ �� �������, ������������� ���� �� ������� ���������� �� MVC, ����� ������ ������ �� ����.
                                               // �� ������ ��������� ����������, ����� �� ��� ������ ��� ���������� �������� ��������� �������, �� ��������� ������ �� �����?
    {
        public UnityAction timeIsOver;
        private int _duration;
        private Thread _thread;
        public BuffTimer(int duration)
        {
            _duration = duration;
            _thread = new Thread(StartTimer); 
            _thread.Start();
        }

        private void StartTimer()
        {
            Stopwatch stopWatch = new Stopwatch();
            int roundIndent = 1;
            int curentTimeDifferense = 0;

            while (curentTimeDifferense <= _duration + roundIndent)
            {
                stopWatch.Start();
                if (curentTimeDifferense >= _duration)
                {
                    timeIsOver?.Invoke();
                }
                stopWatch.Stop();
                curentTimeDifferense += (int)stopWatch.Elapsed.TotalSeconds;
            }
        }

        public void Dispose()
        {
            _thread.Abort();
            _thread.Join();
        }
    }
}
