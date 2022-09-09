using George.Control.Layer;


AudioController audioController = new AudioController();
audioController.StartRecognizer();

while (true)
{
    Console.WriteLine("keep running: "+ audioController.Listen());
}