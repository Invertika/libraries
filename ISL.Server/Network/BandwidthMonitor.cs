using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISL.Server.Network
{
	public class BandwidthMonitor
	{
		int mAmountServerOutput;
		int mAmountServerInput;
		int mAmountClientOutput;
		int mAmountClientInput;
		// map of client to output and input
		//typedef std::map<NetComputer*, std::pair<int, int> > ClientBandwidth;
		//ClientBandwidth mClientBandwidth;

		public BandwidthMonitor()
		{
			//  mAmountServerOutput(0),
			//mAmountServerInput(0),
			//mAmountClientOutput(0),
			//mAmountClientInput(0)
		}

		public int totalInterServerOut()
		{
			return mAmountServerOutput;
		}

		public int totalInterServerIn()
		{
			return mAmountServerInput;
		}

		public int totalClientOut()
		{
			return mAmountClientOutput;
		}

		public int totalClientIn()
		{
			return mAmountClientInput;
		}

		void increaseInterServerOutput(int size)
		{
			mAmountServerOutput+=size;
		}

		void increaseInterServerInput(int size)
		{
			mAmountServerInput+=size;
		}

		void increaseClientOutput(NetComputer nc, int size)
		{
			//mAmountClientOutput += size;
			//// look for an existing client stored
			//ClientBandwidth::iterator itr = mClientBandwidth.find(nc);

			//// if there isnt one, create one
			//if (itr == mClientBandwidth.end())
			//{
			//    std::pair<ClientBandwidth::iterator, bool> retItr;
			//    retItr = mClientBandwidth.insert(std::pair<NetComputer*, std::pair<int, int> >(nc, std::pair<int, int>(0, 0)));
			//    itr = retItr.first;
			//}

			//itr->second.first += size;

		}

		void increaseClientInput(NetComputer nc, int size)
		{
			//mAmountClientInput += size;

			//// look for an existing client stored
			//ClientBandwidth::iterator itr = mClientBandwidth.find(nc);

			//// if there isnt one, create it
			//if (itr == mClientBandwidth.end())
			//{
			//    std::pair<ClientBandwidth::iterator, bool> retItr;
			//    retItr = mClientBandwidth.insert(std::pair<NetComputer*, std::pair<int, int> >(nc, std::pair<int, int>(0, 0)));
			//    itr = retItr.first;
			//}

			//itr->second.second += size;
		}
	}
}