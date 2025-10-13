# DDRスコア管理アプリケーション ドメイン駆動設計仕様書

* **ドキュメントバージョン:** 1.2
* **作成日:** 2025年10月13日
* **作成者:** Gemini & Project Owner

---

## 1. プロジェクトの全体像とアーキテクチャ

### 1.1. はじめに

#### 1.1.1. 目的

本ドキュメントは、DanceDanceRevolution (DDR) のスコアを管理するデスクトップアプリケーションの開発における、ドメイン駆動設計（DDD）に基づいた設計仕様を定義することを目的とする。

このアプリケーションは、プレイヤーが自身のスコアを記録・追跡し、成長を可視化することを支援する。本ドキュメントは、開発チームが共通の理解を持ち、一貫性のある高品質なソフトウェアを構築するための礎となる。

#### 1.1.2. 設計アプローチ

本プロジェクトでは、拡張性と堅牢性を確保するため、クライアント／サーバー型のアーキテクチャを基盤とし、以下の設計アプローチを組み合わせる。

* **クライアント／サーバーアーキテクチャ:** UIやユーザー操作を担当する**クライアント**（WinUIアプリ）と、データ処理や永続化、外部連携を担当する**サーバー**（OCIバックエンド）を明確に分離する。
* **サーバーレス & イベント駆動:** サーバーサイドの処理は、特定のイベント（画像のアップロードなど）をトリガーとして実行されるサーバーレス関数（OCI Functions）を中心とし、効率的なリソース活用とスケーラビリティを実現する。
* **ドメイン駆動設計 (DDD):** ビジネスの核となるロジックを、クライアントとサーバーの両方から参照可能な共有`Domain`レイヤーにカプセル化する。
* **クリーンアーキテクチャ:** クライアント、サーバーそれぞれの内部で、ビジネスロジックが外部の技術詳細に依存しない構造を維持する。
* **CQRS (コマンド・クエリ責務分離):** データの書き込み（コマンド）と読み込み（クエリ）のパスを物理的に分離する。
* **テスト駆動開発 (TDD):** 実装コードの前にテストコードを記述し、品質を確保する。

#### 1.1.3. 技術スタック概要

* **クライアント:**
    * **UI:** WinUI 3
* **サーバー (Oracle Cloud Infrastructure - OCI):**
    * **コンピューティング:** OCI Functions (サーバーレス)
    * **ストレージ:** OCI Object Storage (キャプチャ画像保存)
    * **データベース:** OCI Autonomous Database (Always Free枠)
    * **セキュリティ:** OCI Vault (APIキーなど機密情報の保管)
* **外部サービス:**
    * **画像認識:** Google Gemini API
* **共通:**
    * **言語/プラットフォーム:** C# / .NET 8
    * **テストフレームワーク:** xUnit

### 1.2. アーキテクチャ概要

```mermaid
graph TD
    subgraph WinUI クライアント
        A[1.ホットキーで画像キャプチャ] --> B[WinUI App]
    end

    subgraph "OCI バックエンド"
        D[OCI Object Storage]
        E[OCI Functions]
        F[OCI Vault]
        G[OCI Autonomous Database]
        H[API Gateway + Functions]
    end

    I([External: Gemini API])

    B -- "2.画像をアップロード" --> D
    D -- "3.イベント発生 (トリガー)" --> E
    E -- "5.APIキー取得" --> F
    E <-- "6.画像を解析" --> I
    E -- "7.解析結果をDBに保存" --> G
    
    B -- "8.データを問い合わせ" --> H
    H -- "9.DBからデータをクエリ" --> G
    H -- "10.結果をクライアントに返す" --> B
 ```

#### 1.2.2. WinUIクライアントの内部アーキテクチャ

クライアント自身もクリーンアーキテクチャの原則に従うが、責務はUIとバックエンドとの通信に特化する。

* **Presentation Layer (`.WinUi`):** WinUI 3による画面表示とユーザー操作の受付（MVVM）。
* **Application Layer (`.Application`):** UIからの指示に基づき、バックエンドへのリクエストを組み立てるサービス。
* **Infrastructure Layer (`.Infrastructure`):** OCIバックエンドのAPIを呼び出すためのHTTPクライアントの実装。
* **Domain Layer (`.Domain`):** クライアント側での入力値検証など、一部のドメインロジックをサーバーと共有するために参照する。


## 2. ユビキタス言語

本セクションでは、プロジェクト関係者（開発者、利用者）が共通の理解を持つために定義された用語集を定義する。これらの用語は、ソースコード（クラス名、メソッド名、変数名など）に直接反映されるべきである。

* **ユーザープロファイル (User Profile)**
    * アプリケーションの利用者情報を管理する単位。スコア記録はすべてこのプロファイルに紐づく。`プレイヤー (Player)`と同義。

* **キャプチャ (Capture)**
    * ホットキーによって取得された、DDRのリザルト画面を含むスクリーンショット画像。解析待ちの状態にある一時的なデータ。

* **解析ジョブ (Analysis Job)**
    * `キャプチャ`をGemini APIに送信し、スコア情報を抽出する一連の処理タスク。通常はキューで管理される。

* **解析結果 (Analysis Result)**
    * `解析ジョブ`によって`キャプチャ`から抽出された、構造化された`スコア`データ。サムネイル画像と関連付けられ、ユーザーによる確認・修正を経てDB登録されるまでの一時的なデータ。

* **楽曲 (Song)**
    * DDRに収録されている曲。曲名、アーティスト名などの固有情報を持つ。

* **譜面 (Chart)**
    * 特定の`楽曲`と`難易度`の組み合わせ。`レベル`を持ち、実際にプレイする対象となる。

* **難易度 (Difficulty)**
    * `譜面`の難易度カテゴリ。`BEGINNER`, `BASIC`, `DIFFIC-ULT`, `EXPERT`, `CHALLENGE`のいずれかの値をとる。

* **レベル (Level)**
    * `譜面`の難易度を具体的に示す1から19までの数値。

* **スコア (Score)**
    * 1回のプレイ結果の詳細をまとめて表現する**値オブジェクト**。以下の属性で構成される。
    * **Points:** 100万点満点の数値。会話やUI上では単に「スコア」と呼ばれることが多い。
    * **EXスコア (EX Score):** 判定ごとの配点に基づくスコア。
    * **ランク (Rank):** `AAA`, `AA`などの評価ランク。
    * **判定 (Judgements):** Marvelous, Perfect, Greatなどの判定ごとの回数。
    * **最大コンボ数 (Max Combo):** プレイ中に達成した最大のコンボ数。
    * **クリアタイプ (Clear Type):** 達成状況を示す種別。`FAILED`, `CLEARED`, `FULL_COMBO`, `PERFECT_FULL_COMBO`などの値をとる。

* **スコア記録 (Score Record)**
    * データベースに永続化された、1回ごとのプレイ履歴。誰が(`User Profile`)、いつ、どの`譜面`をプレイし、どのような`スコア`だったかを記録する不変の事実。

* **自己ベスト (Personal High Score)**
    * `ユーザープロファイル`ごと、`譜面`ごとに記録される、個人の最高`スコア`。`Points`を基準に更新される。

* **トップスコア (Top Score)**
    * `譜面`ごとに、全`ユーザープロファイル`の中で最も高い`スコア`。システム全体の最高記録。


## 3. ドメインモデルの詳細

本セクションでは、システムの核となるビジネスルールを表現するオブジェクト（エンティティ、値オブジェクト）と、それらの永続化を抽象化するインターフェース（リポジトリ）について詳細に定義する。

### 3.1. エンティティ (Entities)

エンティティは、一意な識別子（ID）を持ち、ライフサイクルを通じて状態が変化するオブジェクトである。その同一性は、属性ではなくIDによって保証される。

#### `Player` (プレイヤー)
システムにスコアを記録するユーザーを表す。
- `PlayerId` (ID): プレイヤーの一意な識別子
- `PlayerName` (string): プレイヤー名

#### `Song` (楽曲)
DDRに収録されている楽曲を表す。
- `SongId` (ID): 楽曲の一意な識別子
- `Title` (string): 楽曲のタイトル
- `Artist` (string): アーティスト名
- `Status` (SongStatus): 楽曲の状態 (`Active`, `Deleted`など)。論理削除に使用する。

#### `ScoreRecord` (スコア記録)
プレイヤーによる1回ごとのプレイ履歴を表す。一度記録されたら変更されない。
- `ScoreRecordId` (ID): プレイ履歴の一意な識別子
- `PlayerId` (ID): プレイしたプレイヤーのID
- `Chart` (Value Object): プレイした譜面
- `Score` (Value Object): プレイ結果のスコア詳細
- `PlayedAt` (DateTime): プレイした日時

#### `PersonalHighScore` (自己ベスト)
プレイヤーごと、譜面ごとの自己ベスト記録を表す。
- `PersonalHighScoreId` (ID): 自己ベスト記録の一意な識別子
- `PlayerId` (ID): 記録を保持するプレイヤーのID
- `Chart` (Value Object): 対象となる譜面
- `Score` (Value Object): 自己ベストのスコア詳細
- `AchievedAt` (DateTime): この記録を達成した日時

#### `TopScore` (トップスコア)
譜面ごとの、全プレイヤー中での最高記録を表す。
- `TopScoreId` (ID): トップスコア記録の一意な識別子
- `PlayerId` (ID): この記録を達成したプレイヤーのID
- `Chart` (Value Object): 対象となる譜面
- `Score` (Value Object): トップスコアのスコア詳細
- `AchievedAt` (DateTime): この記録を達成した日時

### 3.2. 値オブジェクト (Value Objects)

値オブジェクトは、その属性の組み合わせによって識別される**不変 (Immutable)**なオブジェクトである。IDを持たず、一度作成されたらその値は変更されない。また、自身の不変条件（ルール）を保証する責務を持つ。

#### `Chart` (譜面)
プレイ対象となる、特定の楽曲の特定の難易度を表す。
- `SongId` (ID): 楽曲のID
- `Difficulty` (enum): 難易度 (`BEGINNER`など)
- `Level` (Value Object): レベル

#### `Level` (レベル)
譜面の難易度を示す数値。
- `Value` (int): レベルの値
- **不変条件:** `Value`は`1`から`19`の範囲内でなければならない。このオブジェクトは、不正な値で作成されることを許容しない。

#### `Score` (スコア)
1回のプレイ結果の詳細をまとめて表現する。
- `Points` (int): 100万点満点の数値
- `EXScore` (int): EXスコア
- `Rank` (enum): 評価ランク (`AAA`など)
- `Judgements` (Value Object): 判定ごとの回数を保持するオブジェクト (`Marvelous`, `Perfect`の数など)
- `MaxCombo` (int): 最大コンボ数
- `ClearType` (enum): クリア種別 (`FULL_COMBO`など)

### 3.3. 集約 (Aggregates) とリポジトリインターフェース

**集約 (Aggregate)** は、関連するエンティティと値オブジェクトをまとめた、データ変更の一貫性を保つための単位である。本設計では、シンプルさを保つため、各エンティティをそれぞれ独立した集約のルート（Aggregate Root）として扱う。

**リポジトリ (Repository)** は、集約の永続化（保存や読み込み）を抽象化するインターフェースである。ドメイン層は、データベースなどの具体的な永続化技術を知ることなく、このインターフェースを通じてデータの操作を行う。

#### リポジトリインターフェース一覧 (`Domain`レイヤーに定義)
- `IPlayerRepository`
- `ISongRepository`
- `IScoreRecordRepository`
- `IPersonalHighScoreRepository`
- `ITopScoreRepository`

## 4. 主要なユースケースと実装方針

本セクションでは、システムの主要な機能（ユースケース）と、それを実現するためのアプリケーションサービスを中心とした実装方針を定義する。ユーザーの操作フローに基づき、スコア登録までの流れを複数のユースケースに分割して定義する。

### 4.1. アプリケーション起動とプロファイル選択

アプリケーションを起動し、操作対象となるユーザープロファイルを選択する。

* **担当コンポーネント:** `UserProfileApplicationService`, アプリケーションのセッション管理サービス
* **処理フロー:**
    1.  **[UI]** アプリケーションが起動し、ユーザープロファイルの選択画面（または新規作成画面）を表示する。
    2.  **[Application]** `UserProfileApplicationService.GetAllProfiles()`を呼び出し、既存のプロファイル一覧を取得して画面に表示する。
    3.  **[UI]** ユーザーがリストから自身のプロファイルを選択する。
    4.  **[Application]** 選択された`UserProfile`を、アプリケーションの生存期間中保持するセッション情報として格納する。以降の操作は、すべてこのプロファイルに紐づいて実行される。
    5.  **[UI]** メイン画面に遷移する。

### 4.2. 画面キャプチャと解析ジョブのキューイング

ホットキー入力によりリザルト画面をキャプチャし、非同期解析のためのジョブキューに追加する。

* **担当コンポーネント:** `CaptureApplicationService`, UI層のホットキー監視機能
* **処理フロー:**
    1.  **[UI]** メイン画面表示後、システムはバックグラウンドでホットキー入力を待機する。
    2.  **[UI]** ユーザーが指定されたホットキーを押すと、画面全体のスクリーンショットが撮影される。
    3.  **[UI → Application]** 撮影された画像データが`CaptureApplicationService.QueueCaptureForAnalysis(imageData)`に渡される。
    4.  **[Application]** サービスは画像データを一時フォルダにファイルとして保存し、そのファイルパスを含む新しい`解析ジョブ`を作成する。
    5.  **[Application]** 作成された`解析ジョブ`を、非同期処理のためのキュー（Queue）に追加する。
    6.  **[Application → UI]** UIに通知し、メイン画面の「解析待ちリスト」に新しい`キャプチャ`が追加されたことを表示する。

### 4.3. 非同期でのスコア解析

バックグラウンドで解析ジョブを処理し、スコアデータを抽出する。

* **担当コンポーネント:** `AnalysisJobProcessorService` (バックグラウンドワーカー), `IScoreOcrService`
* **処理フロー:**
    1.  **[Application/Infrastructure]** `AnalysisJobProcessorService`が、ジョブキューを常時監視する。
    2.  **[Application]** キューに新しい`解析ジョブ`があれば、一つ取り出す。
    3.  **[Application → Infrastructure]** `IScoreOcrService`（実体は`GeminiScoreOcrService`）を呼び出し、`キャプチャ`画像の解析を依頼する。
    4.  **[Application]** 解析に成功した場合、構造化された`Score`データを取得し、`解析結果`を`Success`ステータスで作成する。
    5.  **[Application]** 解析に失敗した場合（例: Gemini APIエラー）、エラー情報を取得し、`解析結果`を`Failure`ステータスと`FailureReason`（失敗理由）付きで作成する。
    6.  **[Application]** `キャプチャ`画像のサムネイルを生成し、`解析結果`と紐付けて一時的に保存する。
    7.  **[Application → UI]** UIに通知し、メイン画面の「解析待ちリスト」からアイテムを削除し、「解析済みリスト」に新しい`解析結果`を表示する。`Failure`ステータスの場合は、その旨がわかるように表示する。

### 4.4. 解析結果の確認、手動修正、および単一登録

ユーザーが解析結果を確認し、必要に応じて手動で修正した上で、最終的なスコアをデータベースに登録する。

* **担当コンポーネント:** `ScoreRegistrationApplicationService`, `SongApplicationService`
* **処理フロー:**
    1.  **[UI]** ユーザーが「解析済みリスト」から任意の`解析結果`を選択する。
    2.  **[UI]** `キャプチャ`画像と、解析された`Score`データが入力された編集フォームを持つ詳細画面を表示する。`Failure`ステータスのアイテムの場合、失敗理由も表示する。
    3.  **[UI]** ユーザーは、楽曲DBの情報を元にした検索機能やドロップダウンリストを用いて、対象の`譜面`を正確に選択する。
    4.  **[UI]** ユーザーは表示された`Score`内容を確認し、もし解析結果に誤りがあれば、点数や判定の数などを手動で修正する。
    5.  **[UI]** ユーザーが「登録」ボタンを押す。
    6.  **[UI → Application]** 最終的な`Score`データ、現在の`UserProfile`、そしてユーザーが選択した`Chart`情報が`ScoreRegistrationApplicationService.RegisterScore(...)`に渡される。
    7.  **[Application]** サービスはまず、**重複登録チェック**を実行する（例: 1分以内に同一プロファイル・同一譜面・同一Score内容の記録が存在しないか確認）。重複の可能性がある場合、`DuplicateScoreException`をスローし、UIはユーザーに確認を求める。
    8.  **[Application]** チェックを通過後、サービスは**ドメインロジック（自己ベスト・トップスコアの比較更新）**を実行し、リポジトリを介して`ScoreRecord`等を永続化する。
    9.  **[Application]** 登録成功後、元となった`キャプチャ`画像と`解析結果`を一時領域から削除する。
    10. **[Application → UI]** UIに通知し、「解析済みリスト」から該当アイテムを削除する。

### 4.5. 解析結果の一括登録

複数の解析結果を、詳細確認画面を経由せずにまとめてデータベースに登録する。

* **担当コンポーネント:** `ScoreRegistrationApplicationService`
* **処理フロー:**
    1.  **[UI]** ユーザーが「解析済みリスト」で複数のアイテム（`Success`ステータスのもののみ）を選択し、「一括登録」ボタンを押す。
    2.  **[UI → Application]** 選択された`解析結果`のリストが`ScoreRegistrationApplicationService.RegisterMultipleScores(...)`に渡される。
    3.  **[Application]** サービスはリスト内の各`解析結果`についてループ処理を行う。
        a.  解析された`Score`データと`Chart`情報を信頼し、手動修正なしで登録処理（ユースケース4.4のステップ7〜9と同様）を実行する。
    4.  **[Application → UI]** UIに処理結果（例：「3件中3件の登録に成功しました」）を通知し、リストから成功したアイテムを削除する。

### 4.6. キャプチャまたは解析結果の削除

ユーザーの操作により、不要になったキャプチャや解析結果をリストから削除する。

* **担当コンポーネント:** `CaptureApplicationService`
* **処理フロー:**
    1.  **[UI]** ユーザーが「解析待ちリスト」または「解析済みリスト」で一つ以上のアイテムを選択し、「削除」ボタンを押す。
    2.  **[UI → Application]** 削除対象のアイテムIDが`CaptureApplicationService.DeleteItems(...)`に渡される。
    3.  **[Application]** サービスはIDに基づき、該当する`キャプチャ`画像ファイルや`解析結果`の一時データを物理的に削除する。
    4.  **[Application → UI]** UIに通知し、リストから該当アイテムを削除する。


## 5. プロジェクト構成とテスト戦略

本セクションでは、クライアント／サーバー型のアーキテクチャをC#/.NETで実現するための具体的なソリューションとプロジェクトの構成、およびTDDを実践するためのテスト戦略を定義する。

### 5.1. ソリューション構成

単一のソリューション（`.sln`）内で、クライアント、サーバー、そして両者から共有されるドメインを、ソリューションフォルダを使って明確に分離・管理する。

```
GPScoreTracker.sln
│
├── 📁 docs/
│   └── ARCHITECTURE.md
│
├── 📁 src-shared/ (クライアント/サーバー共有プロジェクト)
│   └── GPScoreTracker.Domain/
│
├── 📁 src-client/ (WinUIクライアントのプロジェクト群)
│   ├── GPScoreTracker.WinUi/
│   ├── GPScoreTracker.Client.Application/
│   └── GPScoreTracker.Client.Infrastructure/
│
├── 📁 src-server/ (OCIバックエンドのプロジェクト)
│   └── GPScoreTracker.Server.Functions/
│
└── 📁 tests/ (テストプロジェクト群)
    ├── GPScoreTracker.Domain.Tests/
    ├── GPScoreTracker.Client.Application.Tests/
    └── GPScoreTracker.Server.Functions.Tests/
```


### 5.2. 各プロジェクトの役割と依存関係

* **共有プロジェクト (`src-shared`)**
    * **`GPScoreTracker.Domain`:**
        * アプリケーションの「心臓部」。ビジネスの核となるルールと概念（エンティティ、値オブジェクト）を定義する。
        * **クライアントとサーバーの両方から参照される唯一のプロジェクト。**

* **クライアントプロジェクト群 (`src-client`)**
    * **`GPScoreTracker.WinUi`:**
        * ユーザーが直接触れる「顔」。UIの表示とユーザー操作の受付を担当し、`Client.Application`層のサービスを呼び出す。
    * **`GPScoreTracker.Client.Application`:**
        * WinUIアプリのユースケースを実現する「調整役」。UIからの指示に基づき、バックエンドAPIの呼び出しなどを調整する。
    * **`GPScoreTracker.Client.Infrastructure`:**
        * OCIバックエンドのAPI Gatewayを呼び出すためのHTTPクライアントなどを実装する。

* **サーバープロジェクト (`src-server`)**
    * **`GPScoreTracker.Server.Functions`:**
        * OCI Functionsとしてデプロイされるサーバーレス関数のコード。画像解析のイベントハンドリング、DBへの書き込み、データ取得APIなど、すべてのバックエンドロジックを含む。Gemini APIやOCI SDKのクライアント実装もここに配置する。

**依存関係のルール:**
クライアントとサーバーは、`Domain`プロジェクトを共有する以外は、互いにプロジェクト参照を行わない。両者の通信は、ネットワーク（HTTPS）を介して行われる。

### 5.3. テスト戦略

各コンポーネントの役割に応じたテストを配置する。

* **`GPScoreTracker.Domain.Tests` (単体テスト):**
    * **目的:** ドメインオブジェクトが持つビジネスルールを検証する。変更なし。

* **`GPScoreTracker.Client.Application.Tests` (単体テスト):**
    * **目的:** WinUIクライアントのアプリケーションサービスのロジックを検証する。
    * **特徴:** バックエンドAPIを呼び出すHTTPクライアントのインターフェースをモック化してテストする。

* **`GPScoreTracker.Server.Functions.Tests` (単体テスト / 結合テスト):**
    * **目的:** サーバーレス関数のロジックと、外部サービス連携を検証する。
    * **特徴:** 関数のコアロジックは、OCI SDKやGemini APIクライアントをモック化して単体テストする。必要に応じて、エミュレータや開発用のクラウド環境に接続する結合テストも行う。

#### 5.3.1. テスト容易性を高める設計原則

* **依存性の注入 (Dependency Injection - DI):** クラスは、依存するコンポーネントをコンストラクタでインターフェースとして受け取る。
* **インターフェースへの依存:** レイヤー間の境界や外部サービスとの通信は、必ずインターフェースを介して行う。

## 6. 実装ガイドライン

本セクションでは、クライアント／サーバー型のアーキテクチャと設計原則を実際のコードに反映させるための、具体的な実装上の指針やベストプラクティスを定義する。

### 6.1. 共有ドメインモデルの実装 (`Domain` レイヤー)

* **値オブジェクト (Value Object) は不変(Immutable)かつ自己検証的であること**
    * すべてのプロパティは `get` のみ、または `private set` / `init` とし、外部から変更できないようにする。これは**関数型言語の不変性（Immutability）の考え方**を取り入れるもので、予期せぬ副作用を防ぎ、システムの予測可能性を高める。
    * オブジェクトが存在するための絶対的なルール（不変条件）は、コンストラクタまたは静的ファクトリメソッド内で検証し、不正な値の場合は例外をスローする。

    ```csharp
    // Level.cs in GPScoreTracker.Domain/ValueObjects/
    public class Level
    {
        public int Value { get; private set; }

        public Level(int value)
        {
            if (value < 1 || value > 19)
            {
                // 不変条件に違反する場合は、オブジェクトの作成を許可しない
                throw new ArgumentOutOfRangeException(nameof(value), "Level must be between 1 and 19.");
            }
            this.Value = value;
        }
    }
    ```

* **エンティティの振る舞い**
    * エンティティ自身の状態のみに依存するビジネスロジックは、エンティティのメソッドとして実装する。
    * 例: `Song`エンティティの`MarkAsDeleted()`メソッドは、自身の`Status`プロパティを変更する。

### 6.2. サーバーサイドの実装ガイドライン (`Server` レイヤー)

* **データアクセスはEF Core Code-Firstアプローチを採用する**
    * `GPScoreTracker.Server.Functions`プロジェクト内のリポジトリ実装は、EF Coreの**コードファースト**アプローチを用いてOCI上のデータベースと連携する。C#で定義されたドメインエンティティがデータベースのスキーマの正となる。

* **クエリはDTOへ直接プロジェクションする**
    * 読み取り専用のクエリでは、パフォーマンス向上のため、EF Coreの`Select`句を使ってドメインエンティティを介さずに直接DTOを構築する。

    ```csharp
    // SongRepository.cs in GPScoreTracker.Server.Functions/Persistence/
    public async Task<List<SongListItemDto>> GetSongListItems()
    {
        // _contextはEF CoreのDbContext
        return await _context.Songs
            .AsNoTracking() // 読み取り専用クエリでは追跡不要
            .Where(s => s.Status == SongStatus.Active)
            .Select(s => new SongListItemDto
            {
                SongId = s.SongId,
                SongTitle = s.Title,
                // ... 他のプロパティのマッピング
            })
            .ToListAsync();
    }
    ```

* **サーバーレス関数は薄く保つ**
    * OCI Functionsのハンドラ（エントリーポイント）の役割は、リクエストの受け取りと、アプリケーションサービスへの処理の委譲に限定する。主要なビジネスフローは、テスト容易性を考慮して、独立したアプリケーションサービスクラスに実装する。

* **機密情報はOCI Vaultから取得する**
    * Gemini APIキーやデータベース接続文字列などの機密情報は、コード内にハードコーディングせず、OCI Functionsの実行時にOCI Vaultから動的に取得する。

### 6.3. クライアントサイドの実装ガイドライン (`Client` レイヤー)

* **バックエンドとの通信はHTTPクライアントを介して行う**
    * `GPScoreTracker.Client.Infrastructure`プロジェクトに、OCI API Gatewayのエンドポイントを呼び出すためのHTTPクライアントを実装する。`.NET`が推奨する`IHttpClientFactory`を利用して、効率的かつ堅牢な通信を実装する。

    ```csharp
    // BackendApiClient.cs in GPScoreTracker.Client.Infrastructure/ApiClients/
    public class BackendApiClient : IBackendApiClient // インターフェースを定義
    {
        private readonly HttpClient _httpClient;

        // DIコンテナからHttpClientが注入される
        public BackendApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SongDetailDto?> GetSongDetailsAsync(Guid songId, Guid userProfileId)
        {
            // APIエンドポイントの具体的なパスは設定ファイルで管理する
            var response = await _httpClient.GetAsync($"api/songs/{songId}?profile={userProfileId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SongDetailDto>();
        }
    }
    ```

* **アプリケーションの状態を管理する**
    * 現在選択されている`UserProfile`など、アプリケーション全体で共有される状態は、専用のサービスクラス（例: `SessionContext`）で一元管理し、DIを通じて各ViewModelに提供する。

* **一時データはローカルに保存後、速やかにアップロードする**
    * `キャプチャ`画像は、まずクライアントPCのローカル一時フォルダに保存する。その後、バックグラウンドで速やかにOCI Object Storageへのアップロード処理を開始し、アップロード成功後はローカルファイルを削除する。

### 6.4. 共通のガイドライン

* **依存性の注入 (DI) を徹底する**
    * **クライアント・サーバーを問わず**、すべてのレイヤーでDIを徹底する。クラスは、依存するコンポーネントをコンストラクタでインターフェースとして受け取る。

* **エラーハンドリング**
    * クライアントとサーバー間の通信では、ネットワークエラーやHTTPステータスコード（4xx, 5xx系）のハンドリングを考慮する。
    * 予期せぬエラーが発生した場合、ユーザーには汎用的なメッセージを表示し、ログには詳細なエラー情報（エラーコード、スタックトレース等）を記録する。

* **コーディングスタイル: 関数型言語の考え方の導入**
    * コードの品質と予測可能性を高めるため、以下の関数型言語の考え方を積極的に取り入れる。
    * **1. 不変性 (Immutability) の重視:** 特に値オブジェクトは不変とし、一度作成されたインスタンスの状態が決して変わらないことを保証する。
    * **2. 副作用の分離:** ロジックを、計算のみを行う純粋な部分と、状態の変更（DBへの書き込み、API呼び出しなど）を行う副作用のある部分に意識的に分離する。CQRSの設計はこの原則に沿っている。
    * **3. 式ベースのプログラミング:** `for`ループのような命令的な文よりも、LINQのような宣言的で式ベースの記述を優先する。これにより、コードの意図が明確になり、可読性が向上する。


## 7. エラー定義

本セクションでは、アプリケーション内で発生しうる主要なエラーを定義し、エラーコードを割り当てる。これにより、一貫性のあるエラーハンドリングを実装する。

### エラーコード体系

エラーコードは `[発生源]_[種別]_[連番]` の形式で定義する。

* **`DOM` (Domain):** 共有ドメイン層で発生する、不変条件違反。
* **`SVR` (Server):** サーバーサイド（OCI Functions）で発生する、ビジネスロジックやインフラのエラー。
* **`CLT` (Client):** クライアントサイド（WinUIアプリ）で発生する、ローカル処理のエラー。
* **`COM` (Communication):** クライアントとサーバー間のネットワーク通信で発生するエラー。

### エラー定義一覧

| エラーコード | 例外クラス (C#) / 状況 | デフォルトメッセージ | 主な発生シナリオ | 主な対処法 |
|:---|:---|:---|:---|:---|
| **Domain Layer** | | | | |
| `DOM_VAL_001` | `ArgumentOutOfRangeException` | レベルは1から19の間でなければなりません。 | 不正な値で`Level`値オブジェクトを生成しようとした。 | 開発時のバグ。基本的には発生しない想定。ログ記録。 |
| `DOM_VAL_002` | `ArgumentOutOfRangeException` | Pointsは0から1,000,000の間でなければなりません。 | 不正な値で`Score`値オブジェクトを生成しようとした。 | 開発時のバグ。ユーザー入力はApp層で検証済みのはず。 |
| **Server Side** | | | | |
| `SVR_BIZ_001` | `DuplicateScoreException` | 短時間のうちに、類似したスコアが既に登録されています。 | スコア登録時に、1分以内の重複データが検知された。 | `409 Conflict`をクライアントに返す。UIで確認を促す。 |
| `SVR_BIZ_002` | `ChartNotFoundException` | 選択された譜面が見つかりませんでした。 | クライアントから送られた`Chart`情報がDBに存在しない。| `404 Not Found`を返す。UIで譜面の再選択を促す。 |
| `SVR_INF_001` | `OcrFailureException` | 画像解析に失敗しました。 | Gemini APIがエラーを返した、または解析不能な画像だった。 | `解析結果`を`Failure`ステータスでDBに保存。UIに理由を表示。|
| `SVR_INF_002` | `DatabaseAccessException` | データベースへのアクセスに失敗しました。 | OCI DBへの接続エラーや予期せぬ問題。 | `500 Internal Server Error`を返す。サーバー側で詳細をログ記録。|
| `SVR_SEC_001` | `SecretFetchException` | 機密情報へのアクセスに失敗しました。 | OCI VaultからAPIキーが取得できない。 | `500 Internal Server Error`を返す。サーバー側で詳細をログ記録。|
| **Client Side** | | | | |
| `CLT_FS_001` | `CaptureStorageException` | キャプチャ画像のローカル保存に失敗しました。 | ディスク容量不足、または書き込み権限がない。 | UIでエラーメッセージ表示。ユーザーにディスクや権限の確認を促す。|
| `CLT_INF_001`| `FileUploadException` | 画像のアップロードに失敗しました。 | OCI Object Storageへのアップロード中にエラーが発生。 | UIでエラーメッセージ表示。ネットワーク状態の確認やリトライを促す。|
| **Communication** | | | | |
| `COM_NET_001` | `HttpRequestException` | ネットワークに接続できません。 | インターネット接続がない、またはDNSの問題。 | UIでオフライン状態であることを通知し、リトライを促す。 |
| `COM_API_404` | HTTP 404 Not Found | APIエンドポイントが見つかりません。 | クライアントが不正なAPIパスにリクエストした。 | 開発時のバグ。UIで汎用エラーを表示し、ログ記録。 |
| `COM_API_500`| HTTP 500 Internal Server Error | サーバーで予期せぬエラーが発生しました。 | サーバー側でキャッチされなかった例外が発生した。| UIで汎用エラーを表示し、時間をおいて試すよう促す。|