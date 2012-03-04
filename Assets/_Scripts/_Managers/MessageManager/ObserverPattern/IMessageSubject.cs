
public interface  IMessageSubject {
	
	 void RegisterObserver(IMessageObsever o);
	 bool RemoveObserver(IMessageObsever o);
	 void NotifyObsevers();
	
}
