"""init_db

Revision ID: a1b2c3d4e5f6
Revises: 
Create Date: 2026-06-06 16:00:00.000000

"""
from typing import Sequence, Union
from alembic import op
import sqlalchemy as sa

# revision identifiers, used by Alembic.
revision: str = "a1b2c3d4e5f6"
down_revision: Union[str, None] = None
branch_labels: Union[str, Sequence[str], None] = None
depends_on: Union[str, Sequence[str], None] = None


def upgrade() -> None:
    # 1. Create templates table
    op.create_table(
        "templates",
        sa.Column("id", sa.String(36), primary_key=True, index=True),
        sa.Column("name", sa.String(100), nullable=False),
        sa.Column("config", sa.JSON(), nullable=False),
        sa.Column("deleted_at", sa.DateTime(), nullable=True),
    )

    # 2. Create users table
    users_table = op.create_table(
        "users",
        sa.Column("id", sa.Integer(), primary_key=True, autoincrement=True),
        sa.Column("username", sa.String(50), nullable=False, unique=True, index=True),
        sa.Column("password_hash", sa.String(100), nullable=False),
        sa.Column("role", sa.String(20), nullable=False, server_default="admin"),
    )

    # 3. Create screens table
    op.create_table(
        "screens",
        sa.Column("id", sa.String(50), primary_key=True, index=True),
        sa.Column("name", sa.String(100), nullable=False),
        sa.Column("complex_id", sa.Integer(), nullable=False),
        sa.Column("building_id", sa.Integer(), nullable=False),
        sa.Column("entrance", sa.Integer(), nullable=True),
        sa.Column("current_template_id", sa.String(36), sa.ForeignKey("templates.id"), nullable=True),
    )

    # 4. Create emergencies table
    op.create_table(
        "emergencies",
        sa.Column("id", sa.String(36), primary_key=True, index=True),
        sa.Column("text", sa.String(500), nullable=False),
        sa.Column("target_screens", sa.JSON(), nullable=False),
        sa.Column("priority", sa.Integer(), nullable=False, server_default="1"),
        sa.Column("timeout_minutes", sa.Integer(), nullable=True),
        sa.Column("activated_by", sa.String(50), nullable=False),
        sa.Column("created_at", sa.DateTime(), nullable=False, server_default=sa.func.now()),
        sa.Column("resolved_at", sa.DateTime(), nullable=True),
    )

    # 5. Seed default admin user (username: admin, password: admin123)
    # Bcrypt hash for 'admin123'
    default_admin_hash = "$2b$12$8Ubp6s3FpT95sPcc42jHF.nSLraASAkXXicFJtlgiU7xqOtzDL3Ju"
    
    op.bulk_insert(
        users_table,
        [
            {
                "username": "admin",
                "password_hash": default_admin_hash,
                "role": "admin",
            }
        ],
    )


def downgrade() -> None:
    op.drop_table("emergencies")
    op.drop_table("screens")
    op.drop_table("users")
    op.drop_table("templates")
