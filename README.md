# GPScoreTracker - DDR Score Management Application

DDR (DanceDanceRevolution) �̃X�R�A���Ǘ�����f�X�N�g�b�v�A�v���P�[�V����

## ?? �v���W�F�N�g�T�v

���̃A�v���P�[�V�����́ADDR�v���C���[�����g�̃X�R�A���L�^�E�ǐՂ��A�������������邱�Ƃ��x�����܂��B

### ��ȋ@�\

- �z�b�g�L�[�ɂ���ʃL���v�`��
- AI�iGemini API�j�ɂ�鎩���X�R�A���
- �X�R�A�����̊Ǘ�
- ���ȃx�X�g�E�g�b�v�X�R�A�̒ǐ�
- �f�[�^�̃G�N�X�|�[�g�iCSV�j

## ??? �A�[�L�e�N�`��

- **�N���C�A���g**: WinUI 3 �f�X�N�g�b�v�A�v��
- **�T�[�o�[**: Oracle Cloud Infrastructure (OCI) �T�[�o�[���X
- **�݌v��@**: �h���C���쓮�݌v�iDDD�j�A�N���[���A�[�L�e�N�`���ACQRS

�ڍׂ� [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) ���Q�Ƃ��Ă��������B

## ??? �Z�p�X�^�b�N

### �N���C�A���g
- .NET 8
- WinUI 3
- C#

### �T�[�o�[
- OCI Functions (�T�[�o�[���X)
- OCI Autonomous Database (Always Free)
- OCI Object Storage
- OCI Vault

### �O���T�[�r�X
- Google Gemini API�i�摜��́j

## ?? �v���W�F�N�g�\��

```
GPScoreTracker.sln
������ docs/       # �h�L�������g
��   ������ ARCHITECTURE.md
������ src-shared/ # ���L�h���C�����W�b�N
��   ������ GPScoreTracker.Domain/
������ src-client/                # �N���C�A���g
��   ������ GPScoreTracker.WinUi/
��   ������ GPScoreTracker.Client.Application/
��   ������ GPScoreTracker.Client.Infrastructure/
������ src-server/         # �T�[�o�[
��   ������ GPScoreTracker.Server.Functions/
������ tests/               # �e�X�g
    ������ GPScoreTracker.Domain.Tests/
    ������ GPScoreTracker.Client.Application.Tests/
    ������ GPScoreTracker.Client.Infrastructure.Tests/
  ������ GPScoreTracker.Server.Functions.Tests/
```

## ?? �Z�b�g�A�b�v�菇

### �K�v�Ȋ�

- .NET 8 SDK
- Visual Studio 2022 (WinUI 3 �T�|�[�g)
- Oracle Cloud Infrastructure �A�J�E���g
- Google Gemini API �L�[

### �N���[���ƃr���h

```bash
# ���|�W�g���̃N���[��
git clone https://github.com/kkana6624/GPScoreTracker.git
cd GPScoreTracker

# �\�����[�V�����̃r���h
dotnet build

# �e�X�g�̎��s
dotnet test
```

### OCI���̃Z�b�g�A�b�v

1. OCI Autonomous Database �̍쐬�iAlways Free�g�j
2. OCI Object Storage �o�P�b�g�̍쐬
3. OCI Vault �̃Z�b�g�A�b�v
4. OCI Functions �̃f�v���C

�ڍׂȎ菇��[docs/SETUP.md](docs/SETUP.md)���Q�Ɓi�쐬�\��j

## ?? �e�X�g

```bash
# ���ׂẴe�X�g�����s
dotnet test

# ����̃v���W�F�N�g�̃e�X�g
dotnet test tests/GPScoreTracker.Domain.Tests/
```

## ?? �J���K�C�h���C��

### �R�[�f�B���O�K��

- �e�X�g�쓮�J���iTDD�j���̗p
- �h���C�����W�b�N�͕s�ρiImmutable�j�������Ƃ���
- LINQ ���g�p�����錾�I�v���O���~���O��D��
- �\�������M���O�iSerilog�j���g�p

�ڍׂ� [docs/ARCHITECTURE.md#6-�����K�C�h���C��](docs/ARCHITECTURE.md#6-�����K�C�h���C��) ���Q��

## ?? �J���t�F�[�Y

- [x] Phase 0: �v���W�F�N�g�\���Ɗ�Ր���
- [ ] Phase 1: �h���C���w�̎���
- [ ] Phase 2: �C���t���X�g���N�`�����
- [ ] Phase 3: �R�A�@�\�̎���
- [ ] Phase 4: �����E�œK��

## ?? ���C�Z���X

���̃v���W�F�N�g�� MIT ���C�Z���X�̉��Ō��J����Ă��܂��B

## ?? �v��

�v�������}���܂��IIssue �� Pull Request �����C�y�ɂǂ����B

## ?? ���₢���킹

- GitHub Issues: https://github.com/kkana6624/GPScoreTracker/issues
