using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy.Threading {
    // TODO: いずれWaitableTimerラップしたい
    // https://qiita.com/smi/items/855bec8dd3d32c112ab7
    public class Timer {
        private const double MIN_INTERVAL_MILLISECONDS = 16;

        public event EventHandler? Tick { add => _tick += value; remove => _tick = value; }
        public int Interval {
            get {
                return (int) _interval.TotalMilliseconds;
            }
            set {
                if (value < MIN_INTERVAL_MILLISECONDS)
                    throw new Exception($"{MIN_INTERVAL_MILLISECONDS}ms以下を設定できません。");

                _interval = new TimeSpan(0, 0, 0, 0, value);
            }
        }

        public bool Enabled => _running;

        private TimeSpan _interval;
        private volatile bool _running = false;
        private event EventHandler? _tick;

        public void Start(DateTime startAt) {
            _running = true;
            Task.Run(() => {
                var nextTriggerAt = startAt;
                while (_running) {
                    while (true) {
                        var rest = (nextTriggerAt - DateTime.Now).TotalMilliseconds;

                        // Sleepメソッドには16msまでの精度しかないため、16msまではSleepで待機し、
                        // それ以降はSpinWaitでCPU負荷を下げる
                        if (rest > MIN_INTERVAL_MILLISECONDS)
                            Thread.Sleep((int)(rest - MIN_INTERVAL_MILLISECONDS));
                        else if (rest > 0)
                            Thread.SpinWait(50);
                        else
                            break;

                    }
                    _tick?.Invoke(this, new EventArgs());

                    nextTriggerAt = nextTriggerAt.AddMilliseconds(_interval.TotalMilliseconds);
                }
            });
        }

        public void Stop() {
            _running = false;
        }
    }

}
