# 🧩 Visualizador 3D (.obj) com WinForms

Aplicação em **C# com Windows Forms** para visualização de modelos 3D em formato `.obj`, com renderização otimizada via acesso direto à memória (`LockBits`).

---

## 🚀 Funcionalidades

- 📂 **Leitura de arquivos `.obj`** (vértices e faces triangulares)
- ⚡ **Renderização em wireframe** com acesso à memória (`LockBits`)
- 🖱️ **Movimentação** (botão esquerdo + arrastar)
- 🎯 **Rotação 3D** (botão direito + arrastar, matriz 4x4 acumulada)
- 🔍 **Zoom** (scroll do mouse)

---

## 🎮 Controles

| Ação                             | Operação                              |
|----------------------------------|---------------------------------------|
| 🖱️ Botão esquerdo + arrastar     | Move o objeto                        |
| 🖱️ Botão direito + arrastar      | Rotaciona (X, Y, Z)                  |
| 🖱️ Scroll do mouse               | Zoom (aumentar/reduzir escala)       |

---

## 🗂️ Estrutura do Projeto

- `Form1.cs` — Interface e eventos do usuário
- `Objeto3D.cs` — Leitura do `.obj`, transformações e desenho otimizado (LockBits)
- `Matriz4x4.cs` — Rotação com matriz acumulada (X, Y, Z)

---

## ⚙️ Requisitos

- Visual Studio 2022 ou superior
- .NET Framework 4.7.2+ ou .NET 6+ (WinForms)
- Windows

---

## 📝 Observações

- Suporte a arquivos `.obj` apenas com faces triangulares
- Zoom, movimento e rotação acumulada por matriz 4x4
- Desenho feito direto na memória para alta performance
- Fácil extensão para futuras funcionalidades (preenchimento, projeção perspectiva, etc.)

---

## 📖 Exemplo de uso

```obj
v 1.000000 1.000000 -1.000000
v -1.000000 1.000000 -1.000000
v -1.000000 1.000000 1.000000
v 1.000000 1.000000 1.000000
f 1 2 3
f 1 3 4
