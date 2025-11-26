using UnityEngine;
using UnityEngine.UI;

public class Opcoes : Menu
{
    public ControladorCRT controladorCRT;
    
    public Toggle checkboxCRT;
    public Slider sliderVolAmb;
    public Slider sliderVolMusica;
    
    private float _volAmbiente;
    private float _volMusica;
    private int _crt;

    private bool _editado;

    public void Awake()
    {
        _crt = Persistencia.CRT();
        _volAmbiente = Persistencia.VolumeAmbiente();
        _volMusica = Persistencia.VolumeMusica();
        
        checkboxCRT.isOn = _crt != 0;
        sliderVolAmb.value = _volAmbiente;
        sliderVolMusica.value = _volMusica;
    }
    
    public void Salvar()
    {
        if (!_editado) return;
        
        Persistencia.SalvarPrefs(_volAmbiente, _volMusica, _crt);
        _editado = false;
        controladorCRT.Atualizar(_crt);
    }

    public void SetCRT(Toggle toggle)
    {
        _editado = true;
        _crt = toggle.isOn ? 1 : 0;
    }

    public void SetVolumeAmbiente(Slider slider)
    {
        _editado = true;
        _volAmbiente = slider.value;
    }
    
    public void SetVolumeMusica(Slider slider)
    {
        _editado = true;
        _volMusica = slider.value;
    }

    public void Voltar() => SelecaoFase.CarregarMenuInicial();
}
