# GPScoreTracker - DDR Score Management Application

DDR (DanceDanceRevolution) のスコアを管理するデスクトップアプリケーション

## ?? プロジェクト概要

このアプリケーションは、DDRプレイヤーが自身のスコアを記録・追跡し、成長を可視化することを支援します。

### 主な機能

- ホットキーによる画面キャプチャ
- AI（Gemini API）による自動スコア解析
- スコア履歴の管理
- 自己ベスト・トップスコアの追跡
- データのエクスポート（CSV）

## ??? アーキテクチャ

- **クライアント**: WinUI 3 デスクトップアプリ
- **サーバー**: Oracle Cloud Infrastructure (OCI) サーバーレス
- **設計手法**: ドメイン駆動設計（DDD）、クリーンアーキテクチャ、CQRS

詳細は [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) を参照してください。

## ??? 技術スタック

### クライアント
- .NET 8
- WinUI 3
- C#

### サーバー
- OCI Functions (サーバーレス)
- OCI Autonomous Database (Always Free)
- OCI Object Storage
- OCI Vault

### 外部サービス
- Google Gemini API（画像解析）

## ?? プロジェクト構造

```
GPScoreTracker.sln
├── docs/       # ドキュメント
│   └── ARCHITECTURE.md
├── src-shared/ # 共有ドメインロジック
│   └── GPScoreTracker.Domain/
├── src-client/                # クライアント
│   ├── GPScoreTracker.WinUi/
│   ├── GPScoreTracker.Client.Application/
│   └── GPScoreTracker.Client.Infrastructure/
├── src-server/         # サーバー
│   └── GPScoreTracker.Server.Functions/
└── tests/               # テスト
    ├── GPScoreTracker.Domain.Tests/
    ├── GPScoreTracker.Client.Application.Tests/
    ├── GPScoreTracker.Client.Infrastructure.Tests/
  └── GPScoreTracker.Server.Functions.Tests/
```

## ?? セットアップ手順

### 必要な環境

- .NET 8 SDK
- Visual Studio 2022 (WinUI 3 サポート)
- Oracle Cloud Infrastructure アカウント
- Google Gemini API キー

### クローンとビルド

```bash
# リポジトリのクローン
git clone https://github.com/kkana6624/GPScoreTracker.git
cd GPScoreTracker

# ソリューションのビルド
dotnet build

# テストの実行
dotnet test
```

### OCI環境のセットアップ

1. OCI Autonomous Database の作成（Always Free枠）
2. OCI Object Storage バケットの作成
3. OCI Vault のセットアップ
4. OCI Functions のデプロイ

詳細な手順は[docs/SETUP.md](docs/SETUP.md)を参照（作成予定）

## ?? テスト

```bash
# すべてのテストを実行
dotnet test

# 特定のプロジェクトのテスト
dotnet test tests/GPScoreTracker.Domain.Tests/
```

## ?? 開発ガイドライン

### コーディング規約

- テスト駆動開発（TDD）を採用
- ドメインロジックは不変（Immutable）を原則とする
- LINQ を使用した宣言的プログラミングを優先
- 構造化ロギング（Serilog）を使用

詳細は [docs/ARCHITECTURE.md#6-実装ガイドライン](docs/ARCHITECTURE.md#6-実装ガイドライン) を参照

## ?? 開発フェーズ

- [x] Phase 0: プロジェクト構造と基盤整備
- [ ] Phase 1: ドメイン層の実装
- [ ] Phase 2: インフラストラクチャ基盤
- [ ] Phase 3: コア機能の実装
- [ ] Phase 4: 統合・最適化

## ?? ライセンス

このプロジェクトは MIT ライセンスの下で公開されています。

## ?? 貢献

貢献を歓迎します！Issue や Pull Request をお気軽にどうぞ。

## ?? お問い合わせ

- GitHub Issues: https://github.com/kkana6624/GPScoreTracker/issues
