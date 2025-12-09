namespace AdventOfCode2025.Tools.IntervalTree;

 internal class IntervalTreeNode<TKey, TValue> : IComparer<RangeValuePair<TKey, TValue>>
    {
        private readonly TKey? _center;

        private readonly IComparer<TKey> _comparer;
        private readonly RangeValuePair<TKey, TValue>[]? _items;
        private readonly IntervalTreeNode<TKey, TValue>? _leftNode;
        private readonly IntervalTreeNode<TKey, TValue>? _rightNode;

        public IntervalTreeNode(IComparer<TKey>? comparer)
        {
            _comparer = comparer ?? Comparer<TKey>.Default;
            _center = default;
            _leftNode = null;
            _rightNode = null;
            _items = null;
        }

        public IntervalTreeNode(IList<RangeValuePair<TKey, TValue>> items, IComparer<TKey>? comparer)
        {
            _comparer = comparer ?? Comparer<TKey>.Default;
            
            var endPoints = new List<TKey>(items.Count * 2);
            foreach (var item in items)
            {
                endPoints.Add(item.From);
                endPoints.Add(item.To);
            }

            endPoints.Sort(this._comparer);
            
            if (endPoints.Count > 0)
            {
                Min = endPoints[0];
                _center = endPoints[endPoints.Count / 2];
                Max = endPoints[^1];
            }

            var inner = new List<RangeValuePair<TKey, TValue>>();
            var left = new List<RangeValuePair<TKey, TValue>>();
            var right = new List<RangeValuePair<TKey, TValue>>();

            foreach (var o in items)
                if (this._comparer.Compare(o.To, _center) < 0)
                    left.Add(o);
                else if (this._comparer.Compare(o.From, _center) > 0)
                    right.Add(o);
                else
                    inner.Add(o);

            if (inner.Count > 0)
            {
                if (inner.Count > 1)
                    inner.Sort(this);
                _items = inner.ToArray();
            }
            else
            {
                _items = null;
            }

            if (left.Count > 0)
                _leftNode = new IntervalTreeNode<TKey, TValue>(left, _comparer);
            if (right.Count > 0)
                _rightNode = new IntervalTreeNode<TKey, TValue>(right, _comparer);
        }

        public TKey? Max { get; }

        public TKey? Min { get; }

        int IComparer<RangeValuePair<TKey, TValue>>.Compare(RangeValuePair<TKey, TValue> x,
            RangeValuePair<TKey, TValue> y)
        {
            var fromComp = _comparer.Compare(x.From, y.From);
            return fromComp == 0 ? _comparer.Compare(x.To, y.To) : fromComp;
        }

        public IEnumerable<TValue> Query(TKey value)
        {
            var results = new List<TValue>();

            if (_items != null)
                foreach (var o in _items)
                    if (_comparer.Compare(o.From, value) > 0)
                        break;
                    else if (_comparer.Compare(value, o.From) >= 0 && _comparer.Compare(value, o.To) <= 0)
                        results.Add(o.Value);

            var centerComp = _comparer.Compare(value, _center);
            if (_leftNode != null && centerComp < 0)
                results.AddRange(_leftNode.Query(value));
            else if (_rightNode != null && centerComp > 0)
                results.AddRange(_rightNode.Query(value));

            return results;
        }

        public IEnumerable<TValue> Query(TKey from, TKey to)
        {
            var results = new List<TValue>();

            if (_items != null)
                foreach (var o in _items)
                    if (_comparer.Compare(o.From, to) > 0)
                        break;
                    else if (_comparer.Compare(to, o.From) >= 0 && _comparer.Compare(from, o.To) <= 0)
                        results.Add(o.Value);

            if (_leftNode != null && _comparer.Compare(from, _center) < 0)
                results.AddRange(_leftNode.Query(from, to));
            if (_rightNode != null && _comparer.Compare(to, _center) > 0)
                results.AddRange(_rightNode.Query(from, to));

            return results;
        }
    }